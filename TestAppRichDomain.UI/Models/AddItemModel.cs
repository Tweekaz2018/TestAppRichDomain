using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.UI.Helpers;

namespace TestAppRichDomain.UI.Models
{
    public class AddItemModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [FileExtensionCheck("jpg,jpeg,png", ErrorMessage = "Need to be jpg, jpeg or png")]
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}