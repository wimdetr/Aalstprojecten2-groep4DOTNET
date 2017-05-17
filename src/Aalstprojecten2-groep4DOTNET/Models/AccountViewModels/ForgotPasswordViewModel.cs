using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="E-mail is verplicht")]
        [Display(Name ="E-mail *")]
        [EmailAddress(ErrorMessage = "E-mail moet een geldig e-mail adres zijn.")]
        public string Email { get; set; }
    }
}
