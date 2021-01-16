using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Models;

namespace TestAppRichDomain.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Item> _itemRepository;
        private int PageSize;
        public HomeController(ILogger<HomeController> logger, IRepository<Item> itemRepository, int pageSize = 3)
        {
            _logger = logger;
            _itemRepository = itemRepository;
            PageSize = pageSize;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var items = (await _itemRepository.ListAllAsync()).Select(x => new ItemModel(x));
            PageViewModel<ItemModel> model = new PageViewModel<ItemModel>(items, page, PageSize);
            return View(model);
        }

        public async Task<IActionResult> Item(int id)
        {
            ItemModel item = new ItemModel(await _itemRepository.GetByIdAsync(id));
            return View(item);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
