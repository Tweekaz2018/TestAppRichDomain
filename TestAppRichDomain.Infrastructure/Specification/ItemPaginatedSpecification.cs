using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Specification
{
    public class ItemPaginatedSpecification : Specification<Item>
    {
        public ItemPaginatedSpecification(int skip, int take)
               : base()
        {
            Query
                .Skip(skip)
                .Take(take);
        }
    }
}
