using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Specification;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Interfaces;
using TestAppRichDomain.UI.Models;

namespace TestAppRichDomain.UI.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Item> _itemRepository;
        public BasketViewModelService(IRepository<Basket> basketRepository, IRepository<Item> itemRepository)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task<BasketModel> GetBasket(string username)
        {
            var basketSpec = new BasketWithItemsSpecification(username);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            if(basket == null)
            {
                basket = new Basket(username);
                await _basketRepository.AddAsync(basket);
            }

            return new BasketModel()
            {
                BasketId = basket.Id,
                BasketItems = await GetBasketItems(basket),
                OwnerId = username
            };

        }

        private async Task<IEnumerable<BasketItemsModel>> GetBasketItems(Basket basket)
        {
            var itemSpec = new ItemSpecification(basket.BasketItems.Select(x => x.ItemId));
            var items = await _itemRepository.ListAsync(itemSpec);

            return basket.BasketItems.Select(x => new BasketItemsModel()
            {
                ItemId = x.ItemId,
                Price = x.Price,
                Quantity = x.Quantity,
                Title = items.First().Title
            });
        }
    }
}
