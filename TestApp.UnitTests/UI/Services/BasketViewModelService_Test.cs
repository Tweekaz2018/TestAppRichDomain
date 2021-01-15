using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Models;
using TestAppRichDomain.UI.Services;
using Xunit;


namespace TestApp.UnitTests.UI.Services
{
    public class BasketViewModelService_Test
    {

        private readonly Mock<IRepository<Basket>> _basketRepository = new Mock<IRepository<Basket>>();
        private readonly Mock<IRepository<Item>> _itemRepository = new Mock<IRepository<Item>>();
        private readonly SpecificationEvaluator<Basket> _evaluator = new SpecificationEvaluator<Basket>();
        private readonly List<Basket> _baskets = new List<Basket>();
        private string _username = "username";

        public BasketViewModelService_Test()
        {
            _basketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Basket>>()))
                .ReturnsAsync((ISpecification<Basket> spec) =>
                {
                    var result = _evaluator.GetQuery(_baskets.AsQueryable(), spec).ToList();
                    return result.FirstOrDefault();
                });        
        }

        [Fact]
        public async Task GetBasket_Not_Exist_Basket_Test()
        {
            var basketViewModelService = new BasketViewModelService(_basketRepository.Object, _itemRepository.Object);

            var result = await basketViewModelService.GetBasket(_username);

            _basketRepository.Verify(x => x.AddAsync(It.IsAny<Basket>()));
            Assert.IsType<BasketModel>(result);
            Assert.Equal(_username, result.OwnerId);
        }
        [Fact]
        public async Task GetBasket_Basket_Test()
        {
            var basketViewModelService = new BasketViewModelService(_basketRepository.Object, _itemRepository.Object);
            _baskets.Add(new Basket(_username));

            var result = await basketViewModelService.GetBasket(_username);

            Assert.IsType<BasketModel>(result);
            Assert.Equal(_username, result.OwnerId);
        }
    }
}
