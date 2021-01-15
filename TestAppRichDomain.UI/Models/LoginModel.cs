using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppRichDomain.UI.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email: ")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}
