using System;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Infrastructure;
using TestAppRichDomain.Infrastructure.Specification;
using TestAppRichDomain.Services.Interfaces;

namespace TestAppRichDomain.Services.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;

        public BasketService(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task CreateBasket(string login)
        {
            await _basketRepository.AddAsync(new Basket(login));
        }

        public async Task AddItemToBasket(string login, int itemId, decimal price, int quantity = 1)
        {
            var basketSpec = new BasketWithItemsSpecification(login);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            if (basket == null)
            {
                basket = new Basket(login);
                await _basketRepository.AddAsync(basket);
            }
            basket.AddItem(itemId, price, quantity);

            await _basketRepository.UpdateAsync(basket);
        }

        public async Task ClearBasket(string login)
        {
            var basketSpec = new BasketWithItemsSpecification(login);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            await _basketRepository.DeleteAsync(basket);

        }

        public async Task IncreaseItemInBasket(string login, int itemId, decimal price)
        {
            var basketSpec = new BasketWithItemsSpecification(login);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            basket.AddItem(itemId, price, 1);
        }
    }