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
    public class ItemPaginatedSpecification_Tests
    {
        SpecificationEvaluator<Item> _evaluator = new SpecificationEvaluator<Item>();
        private int _skip = 2;
        private int _take = 3;

        [Fact]
        public void ItemSpecification_Test()
        {
            var spec = new ItemPaginatedSpecification(_skip, _take);

            var result = _evaluator.GetQuery(GetItems().AsQueryable(), spec).ToList();

            Assert.Equal(_take, result.Count);
        }

        public List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("qwe", "descr", "image.jpg", 1, true),
                new Item("qwe", "descr", "image.jpg", 1, true),
                new Item("qwe", "descr", "image.jpg", 1, true),
                new Item("qwe", "descr", "image.jpg", 1, true),
                new Item("qwe", "descr", "image.jpg", 1, true)
            };
        }
    }
}
