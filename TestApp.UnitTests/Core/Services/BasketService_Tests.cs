using Ardalis.Specification;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Services;
using TestAppRichDomain.Shared;
using Xunit;

namespace TestApp.UnitTests.Core.Services
{
    public class BasketService_Tests
    {
        private readonly Mock<IRepository<Item>> _itemRepo = new Mock<IRepository<Item>>();
        private readonly Mock<IRepository<Basket>> _basketRepo = new Mock<IRepository<Basket>>();

        private readonly string _userName = "username";
        private readonly int _itemId = 1;
        private readonly int _priceItem = 10;

        private readonly int _basketId = 1;

        private Basket basket;

        public BasketService_Tests()
        {
            basket = new Basket(_userName) { Id = _basketId };

            _basketRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Basket>>()))
                .ReturnsAsync(() =>
                {
                    return basket;
                });
            _basketRepo.Setup(x => x.AddAsync(It.IsAny<Basket>()));
            _basketRepo.Setup(x => x.UpdateAsync(It.IsAny<Basket>()));

            _itemRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new Item("Title", "Descr", "image.jpg", _priceItem, true) { Id = _itemId });
        }

        [Fact]
        public async Task AddItem_Test()
        {
            var basketService = new BasketService(_basketRepo.Object, _itemRepo.Object);

            await basketService.AddItem(_userName, _itemId);

            _basketRepo.Verify(x => x.UpdateAsync(basket));
            Assert.Equal(1, basket.BasketItems.Count);
        }
    }
}
