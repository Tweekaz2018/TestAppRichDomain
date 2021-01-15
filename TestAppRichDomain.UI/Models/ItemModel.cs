using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.UI.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsAviable { get; set; }

        public ItemModel(Item item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            Image = item.Image;
            Price = item.Price;
            IsAviable = item.IsAviable;
        }

    }
}
