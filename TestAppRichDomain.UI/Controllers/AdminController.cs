using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Interfaces;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Models;

namespace TestAppRichDomain.UI.Controllers
{
    [Authorize(Roles ="Admin")] 
    public class AdminController : Controller
    {

        private readonly IRepository<Item> _itemRepository;
        private readonly ISaveImage _saveFile;
        private readonly ILogger<Item> _logger;
        private readonly IWebHostEnvironment _env;
        public AdminController(IRepository<Item> itemRepository, ISaveImage saveImage,IWebHostEnvironment env, ILogger<Item> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            _saveFile = saveImage;
            _env = env;
        }


        public async Task<IActionResult> ChangeItemState(int itemId, bool isAviable)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (isAviable)
                item.SetAviable();
            else
                item.SetUnAviable();
            await _itemRepository.UpdateAsync(item);
            _logger.LogInformation("Changed item state", itemId, item.IsAviable);
            return RedirectToAction("Item", "Home", new { id = itemId });
        }


        public async Task<IActionResult> AddItem(AddItemModel item)
        {
            if (ModelState.IsValid)
            {
                Stream stream = new MemoryStream();
                await item.Image.CopyToAsync(stream);
                string imagePath = await _saveFile.SaveImage(_env.WebRootPath, "Images", item.Image.FileName, stream);
                await _itemRepository.AddAsync(new Item(item.Title, item.Description, imagePath, item.Price, true));
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }


    }
}
