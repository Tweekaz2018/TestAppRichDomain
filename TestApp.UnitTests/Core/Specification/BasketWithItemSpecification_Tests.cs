using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;
using Ardalis.GuardClauses;
using Ardalis.Specification.EntityFrameworkCore;
using Moq;
using Xunit;
using TestAppRichDomain.Core.Specification;
using System.Linq;

namespace TestApp.UnitTests.Core.Specification
{
    public class BasketWithItemSpecification_Tests
    {
        SpecificationEvaluator<Basket> _evaluator = new SpecificationEvaluator<Basket>();
        private string _username = "username";
        private int _basketId = 3;

        [Fact]
        public void BasketWithItemsSpecification_Username_Test()
        {
            var spec = new BasketWithItemsSpecification(_username);

            var result = _evaluator.GetQuery(GetBaskets().AsQueryable(), spec).FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_username, result.UserId);
        }
        [Fact]
        public void BasketWithItemsSpecification_BasketId_Test()
        {
            var spec = new BasketWithItemsSpecification(_basketId);

            var result = _evaluator.GetQuery(GetBaskets().AsQueryable(), spec).FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_basketId, result.Id);
        }
        [Fact]
        public void BasketWithItemsSpecification_Bad_Username_Test()
        {
            var spec = new BasketWithItemsSpecification("qweqweqwe");

            var result = _evaluator.GetQuery(GetBaskets().AsQueryable(), spec).Any();

            Assert.False(result);
        }
        [Fact]
        public void BasketWithItemsSpecification_Bad_BasketId_Test()
        {
            var spec = new BasketWithItemsSpecification(10000);

            var result = _evaluator.GetQuery(GetBaskets().AsQueryable(), spec).Any();

            Assert.False(result);
        }

        public List<Basket> GetBaskets()
        {
            return new List<Basket>()
            {
                new Basket(_username){Id = 1},
                new Basket(_username){Id = 2},
                new Basket(_username){Id = _basketId}
            };
        }
    }
}
