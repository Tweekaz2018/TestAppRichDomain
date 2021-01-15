using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Interfaces;
using TestAppRichDomain.Core.Specification;
using TestAppRichDomain.Shared;

namespace TestAppRichDomain.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Item> _itemRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<Basket> basketRepository, IRepository<Item> itemRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(int basketId, string comment, string address)
        {
            var basketSpec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            var itemSpec = new ItemSpecification(basket.BasketItems.Select(x => x.ItemId));
            var items = await _itemRepository.ListAsync(itemSpec);

            List<OrderItem> itemsToOrder = basket.BasketItems.Select(x => {
                var item = items.First(i => x.ItemId == i.Id);
                var orderItem = new OrderItem(new OrderItemOrdered(item.Id, item.Title), x.Price, x.Quantity);
                return orderItem;
            }).ToList();

            await _basketRepository.DeleteAsync(basket);

            Order order = new Order(basket.UserId, address, comment, itemsToOrder);

            _ = await _orderRepository.AddAsync(order);
        }
    }
}
