using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Interfaces;
using TestAppRichDomain.Core.Specification;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Interfaces;
using TestAppRichDomain.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TestAppRichDomain.UI.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly ILogger<Basket> _logger;
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        public BasketController(IRepository<Basket> basketRepository, IBasketViewModelService basketViewModelService, IOrderService orderService, IBasketService basketService ,ILogger<Basket> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _basketViewModelService = basketViewModelService;
            _orderService = orderService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.Identity.Name;
            var model = await _basketViewModelService.GetBasket(userId);
            return View(model);
        }

        public async Task<IActionResult> RemoveItem(int itemId)
        {
            string userId = User.Identity.Name;
            var basketSpec = new BasketWithItemsSpecification(userId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            basket.RemoveItem(itemId);
            await _basketRepository.UpdateAsync(basket);
            return RedirectToAction("Index", "Basket");
        }

        public async Task<IActionResult> AddItem(int itemId)
        {
            string userId = User.Identity.Name;
            await _basketService.AddItem(userId, itemId);
            return RedirectToAction("Index", "Basket");
        }

        public async Task<IActionResult> MakeOrder(BasketModel orderRequest)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(orderRequest.BasketId, orderRequest.Comment, orderRequest.Address);
                return View("OrderSuccess");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
