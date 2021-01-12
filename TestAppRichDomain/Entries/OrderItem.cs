using System;
using System.Collections.Generic;
using System.Text;

namespace TestAppRichDomain.Core.Entries
{
    public class OrderItem : BaseEntity
    {
        public int ItemOrdered { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(int itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = Price;
            Quantity = quantity;
        }
    }
}