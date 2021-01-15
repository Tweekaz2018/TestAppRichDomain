using Ardalis.Specification;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Services;
using TestAppRichDomain.Shared;
using Xunit;

namespace TestApp.UnitTests.Core.Services
{
    public class OrderService_Tests
    {
        private readonly Mock<IRepository<Item>> _itemRepo = new Mock<IRepository<Item>>();
        private readonly Mock<IRepository<Basket>> _basketRepo = new Mock<IRepository<Basket>>();
        private readonly Mock<IRepository<Order>> _orderRepo = new Mock<IRepository<Order>>();

        private readonly string _userName = "username";

        private readonly int _itemId = 1;
        private readonly int _priceItem = 10;

        private readonly int _basketId = 1;

        private Order order;
        private Basket basket;
        public OrderService_Tests()
        {
            basket = new Basket(_userName) { Id = _basketId };
            basket.AddItem(_itemId, 2, _priceItem);

            _basketRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Basket>>()))
                .ReturnsAsync(() =>
                {
                    return basket;
                });
            _basketRepo.Setup(x => x.DeleteAsync(It.IsAny<Basket>()));


            _itemRepo.Setup(x => x.ListAsync(It.IsAny<ISpecification<Item>>()))
                .ReturnsAsync(() => new List<Item>() { new Item("Title", "Descr", "image.jpg", _priceItem, true) { Id = _itemId } });

            _orderRepo.Setup(x => x.AddAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order order) => this.order = order);
        }

        [Fact]
        public async Task MakeOrder_Test()
        {
            var orderService = new OrderService(_orderRepo.Object, _basketRepo.Object, _itemRepo.Object);

            await orderService.CreateOrderAsync(_basketId, "", "");

            Assert.Equal(basket.BasketItems.Count, order.OrderItems.Count);
            Assert.Equal(basket.UserId, order.UserId);
            _basketRepo.Verify(x => x.DeleteAsync(It.IsAny<Basket>()));
            _orderRepo.Verify(x => x.AddAsync(It.IsAny<Order>()));

        }
    }
}
