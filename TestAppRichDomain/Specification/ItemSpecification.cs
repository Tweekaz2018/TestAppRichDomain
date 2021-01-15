using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Core.Specification
{
    public sealed class ItemSpecification : Specification<Item>
    {
        public ItemSpecification(IEnumerable<int> ids)
        {
            Query
                .Where(x => ids.Contains(x.Id));
        }
    }
}
