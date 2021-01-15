using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAppRichDomain.Core.Entries;
using Xunit;

namespace TestApp.UnitTests.Entries
{
    public class Basket_Tests
    {
        private readonly string _username = "usersname";
        private readonly int _itemId = 1;
        private readonly decimal _price = 10;
        private readonly int _quantity = 10;
        [Fact]
        public void AddItem_To_Basket_Test()
        {
            var basket = new Basket(_username);
            basket.AddItem(_itemId , _price);

            var item = basket.BasketItems.First();

            Assert.Equal(_itemId, item.ItemId);
            Assert.Equal(_price, item.Price);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void AddItem_To_Basket_With_Quantity_Test()
        {
            var basket = new Basket(_username);
            basket.AddItem(_itemId, _price, _quantity);
            var item = basket.BasketItems.First();

            Assert.Equal(_itemId, item.ItemId);
            Assert.Equal(_price, item.Price);
            Assert.Equal(_quantity, item.Quantity);
        }
        [Fact]
        public void AddItem_ToBasket_Twice_Test()
        {

            var basket = new Basket(_username);
            basket.AddItem(_itemId, _price, _quantity);
            basket.AddItem(_itemId, _price, _quantity);

            var item = basket.BasketItems.First();

            Assert.Equal(1, basket.BasketItems.Count);
            Assert.Equal(_itemId, item.ItemId);
            Assert.Equal(_price, item.Price);
            Assert.Equal(_quantity * 2, item.Quantity);
        }

        [Fact]
        public void RemoveItem_From_Basket_Test()
        {

            var basket = new Basket(_username);
            basket.AddItem(_itemId, _price);

            basket.RemoveItem(_itemId);

            Assert.Equal(0, basket.BasketItems.Count);
        }

    }
}
