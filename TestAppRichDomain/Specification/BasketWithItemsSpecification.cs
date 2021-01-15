using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Core.Specification
{
    public sealed class BasketWithItemsSpecification : Specification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            Query
                .Where(b => b.Id == basketId)
                .Include(b => b.BasketItems);
        }

        public BasketWithItemsSpecification(string userId)
        {
            Query
                .Where(b => b.UserId == userId)
                .Include(b => b.BasketItems);
        }
    }
}
