using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.UI.Interfaces;
using TestAppRichDomain.UI.Models;
using TestAppRichDomain.Core.Specification;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Shared;

namespace TestAppRichDomain.UI.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderViewModelService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderModel>> GetOrders(string username)
        {
            var orderSpec = new CustomerOrdersWithItemsSpecification(username);
            var orders = await _orderRepository.ListAsync(orderSpec);

            List<OrderModel> ordersModel = new List<OrderModel>();
            List<int> itemIds = new List<int>();
            foreach (var order in orders)
            {
                var orderModel = new OrderModel()
                {
                    OrderId = order.Id
                };
                orderModel.OrderItems = order.OrderItems.Select(x => new OrderItemModel()
                {
                    Title = x.Item.Title,
                    ItemId = x.Item.ItemId,
                    Price = x.Price,
                    Quantity = x.Quantity
                });
                ordersModel.Add(orderModel);
            }
            return ordersModel;
        }
    }
}
