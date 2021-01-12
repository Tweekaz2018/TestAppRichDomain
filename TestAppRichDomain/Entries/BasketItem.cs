using System;
using System.Collections.Generic;
using System.Text;

namespace TestAppRichDomain.Core.Entries
{
    public class BasketItem : BaseEntity
    {
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public int ItemId { get; private set; }
        public int BasketId { get; private set; }

        public BasketItem(int itemId, int quantity, decimal price)
        {
            ItemId = itemId;
            //BasketId = basketId;
            Quantity = quantity;
            Price = price;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
