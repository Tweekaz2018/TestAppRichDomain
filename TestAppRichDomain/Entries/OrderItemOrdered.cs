using System;
using System.Collections.Generic;
using System.Text;

namespace TestAppRichDomain.Core.Entries
{
    public class OrderItemOrdered
    {
        public int ItemId { get; private set; }
        public string Title { get; private set; }
        protected OrderItemOrdered()
        {
            //EF
        }

        public OrderItemOrdered(int itemId, string title)
        {
            ItemId = itemId;
            Title = title;
        }
    }
}
