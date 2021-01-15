using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Interfaces;
using TestAppRichDomain.Core.Specification;
using TestAppRichDomain.Shared;

namespace TestAppRichDomain.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Item> _itemRepository;

        public BasketService(IRepository<Basket> basketRepository, IRepository<Item> itemRepository)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }
        public async Task AddItem(string userId, int itemId, int quantity = 1)
        {
            var basketSpec = new BasketWithItemsSpecification(userId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (basket == null)
            {
                basket = new Basket(userId);
                await _basketRepository.AddAsync(basket);
            }
            if (item != null)
            {
                basket.AddItem(itemId, item.Price, quantity);
                await _basketRepository.UpdateAsync(basket);
            }
        }
    }
}
