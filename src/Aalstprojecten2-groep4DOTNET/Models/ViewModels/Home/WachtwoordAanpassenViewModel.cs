using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class WachtwoordAanpassenViewModel
    {
        [Required(ErrorMessage ="Oud wachtwoord is verplicht.")]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens 6 en maximum 100 karakters hebben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Oud wachtwoord *")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nieuw wachtwoord is verplicht.")]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens 6 en maximum 100 karakters hebben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord *")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bevestig wachtwoord is verplicht.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Nieuw en bevestig wachtwoord moeten hetzelfde zijn.")]
        [Display(Name = "Bevestig nieuw wachtwoord *")]
        public string ConfirmPassword { get; set; }
    }
}
