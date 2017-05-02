using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Wachtwoord is verplicht")]
        [Display(Name ="Wachtwoord")]
        [StringLength(100,ErrorMessage = "Het wachtwoord moet minstens 6 en hoogstens 100 karakters lang zijn.", MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "De twee ingegeven wachtwoorden zijn niet hetzelfde.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
