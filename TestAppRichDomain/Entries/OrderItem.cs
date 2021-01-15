using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Shared;

namespace TestAppRichDomain.Core.Entries
{
    public class OrderItem : BaseEntity
    {
        public OrderItemOrdered Item { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(OrderItemOrdered itemOrdered, decimal price, int quantity)
        {
            Item = itemOrdered;
            Price = price;
            Quantity = quantity;
        }
    }
}