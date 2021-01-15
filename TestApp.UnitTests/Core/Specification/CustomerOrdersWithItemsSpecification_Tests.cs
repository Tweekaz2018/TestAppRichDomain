using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Specification;
using Xunit;

namespace TestApp.UnitTests.Core.Specification
{
    public class CustomerOrdersWithItemsSpecification_Tests
    {

        SpecificationEvaluator<Order> _evaluator = new SpecificationEvaluator<Order>();
        private string _username = "username";
        private int _orderId = 3;
        [Fact]
        public void CustomerOrdersWithItemsSpecification_Username_Test()
        {
            var spec = new CustomerOrdersWithItemsSpecification(_username);

            var result = _evaluator.GetQuery(GetOrders().AsQueryable(), spec).FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_username, result.UserId);
            Assert.Equal(_orderId, result.Id);
        }
        [Fact]
        public void CustomerOrdersWithItemsSpecification_Bad_Username_Test()
        {
            var spec = new CustomerOrdersWithItemsSpecification("sadad");

            var result = _evaluator.GetQuery(GetOrders().AsQueryable(), spec).Any();

            Assert.False(result);
        }
        public List<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order("123", "", "", new List<OrderItem>()),
                new Order("234" , "", "", new List<OrderItem>()),
                new Order(_username, "", "", new List<OrderItem>()){Id = _orderId }
            };
        }
    }
}
