﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Specification
{
    public sealed class CustomerOrdersWithItemsSpecification : Specification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string userId)
        {
            Query.Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.ItemOrdered);
        }
    }
}
