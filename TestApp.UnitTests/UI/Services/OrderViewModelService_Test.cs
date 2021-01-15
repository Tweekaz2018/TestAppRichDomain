using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Models;
using TestAppRichDomain.UI.Services;
using Xunit;

namespace TestApp.UnitTests.UI.Services
{
    public class OrderViewModelService_Test
    {
        private readonly Mock<IRepository<Order>> _orderRepository = new Mock<IRepository<Order>>();
        private readonly SpecificationEvaluator<Order> _evaluator = new SpecificationEvaluator<Order>();
        private string _username = "username";

        public OrderViewModelService_Test()
        {
            _orderRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Order>>()))
                .ReturnsAsync((ISpecification<Order> spec) =>
                {
                    var result = _evaluator.GetQuery(GetOrders().AsQueryable(), spec).ToList();
                    return result;
                });
        }
        [Fact]
        public async Task GetOrders_Test()
        {
            var orderViewModelService = new OrderViewModelService(_orderRepository.Object);
            var orderList = GetOrders().Where(x => x.UserId == _username).ToList();
            var order = orderList.First();

            var ordersModel = (await orderViewModelService.GetOrders(_username));
            var orderModel = ordersModel.First();

            Assert.Equal(orderList.Count, ordersModel.Count);
            Assert.Equal(order.OrderItems.Count, orderModel.OrderItems.Count());
            Assert.IsType<List<OrderModel>>(ordersModel);
        }

        public List<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order(_username, "","", new List<OrderItem>(){
                    new OrderItem(new OrderItemOrdered(1, "new item"), 10, 1)
                }),
                new Order("123", "","", new List<OrderItem>(){
                    new OrderItem(new OrderItemOrdered(2, "item"), 10, 1)
                })
            };
        }
    }
}
