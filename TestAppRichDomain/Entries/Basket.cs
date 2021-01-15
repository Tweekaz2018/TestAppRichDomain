using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAppRichDomain.Shared;

namespace TestAppRichDomain.Core.Entries
{
    public class Basket : BaseEntity
    {
        public string UserId { get; private set; }

        private readonly List<BasketItem> _basketItems = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> BasketItems => _basketItems.AsReadOnly();


        public Basket(string userId)
        {
            UserId = userId;
        }

        public void AddItem(int itemId, decimal price, int quantity = 1)
        {
            var item = BasketItems.FirstOrDefault(x => x.ItemId == itemId);
            if (item != null)
                item.AddQuantity(quantity);
            else
                _basketItems.Add(new BasketItem(itemId, quantity, price, Id));
        }
        public void RemoveItem(int itemId)
        {
            var item = BasketItems.FirstOrDefault(x => x.ItemId == itemId);
            if (item != null)
                _basketItems.Remove(item);
        }
    }
}
