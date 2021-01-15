using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppRichDomain.UI.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email: ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Don't compare")]
        [DataType(DataType.Password)]
        [Display(Name = "Retype password: ")]
        public string PasswordConfirm { get; set; }
    }
}
