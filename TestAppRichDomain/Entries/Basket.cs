using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAppRichDomain.Core.Entries
{
    public class Basket : BaseEntity
    {
        public string UserId { get; private set; }

        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();


        public Basket(string userId)
        {
            UserId = userId;
        }

        public void AddItem(int itemId, decimal price, int quantity = 1)
        {
            var item = Items.FirstOrDefault(x => x.ItemId == itemId);
            if (item != null)
                item.AddQuantity(quantity);
            _items.Add(new BasketItem(itemId, quantity, price));
        }
        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }
    }
}
