using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;
using Xunit;

namespace TestApp.UnitTests.Entries
{
    public class Item_Tests
    {
        [Fact]
        public void Item_SetAvailable_Test()
        {
            Item item = new Item("Title", "Descr", "/image/img.jpg", 123, false);

            item.SetAviable();

            Assert.True(item.IsAviable);
        }

        [Fact]
        public void Item_SetUnavailable_Test()
        {
            Item item = new Item("Title", "Descr", "/image/img.jpg", 123, true);

            item.SetUnAviable();

            Assert.False(item.IsAviable);
        }
    }
}
