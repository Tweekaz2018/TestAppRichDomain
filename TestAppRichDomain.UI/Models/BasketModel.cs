using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppRichDomain.UI.Models
{
    public class BasketModel
    {
        [Required]
        public int BasketId { get; set; }
        [Required]
        [Display(Name ="Delivery address")]
        public string Address { get; set; }
        [Display(Name ="You can add comment to your order")]
        public string Comment { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<BasketItemsModel> BasketItems { get; set; }
    }
}
