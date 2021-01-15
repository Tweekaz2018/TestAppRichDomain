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
    public class ItemSpecification_Tests
    {
        SpecificationEvaluator<Item> _evaluator = new SpecificationEvaluator<Item>();
        private int _itemId = 1;

        [Fact]
        public void ItemSpecification_Test()
        {
            var spec = new ItemSpecification(new List<int>() { _itemId });

            var result = _evaluator.GetQuery(GetItems().AsQueryable(), spec).FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_itemId, result.Id);
        }
        [Fact]
        public void ItemSpecification_Bad_Id_Test()
        {
            var spec = new ItemSpecification(new List<int>() { 1000 });

            var result = _evaluator.GetQuery(GetItems().AsQueryable(), spec).Any();

            Assert.False(result);
        }

        public List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("qwe", "descr", "image.jpg", 1, true){Id = _itemId},
                new Item("qwe", "descr", "image.jpg", 1, true)
            };
        }
    }
}
