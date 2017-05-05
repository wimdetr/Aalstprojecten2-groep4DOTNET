using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class ContacteerAdminViewModel
    {
        [Required(ErrorMessage ="Onderwerp is verplicht")]
        [MaxLength(100, ErrorMessage = "Onderwerp mag maximaal 100 karakters bevatten!")]
        [Display(Name ="Onderwerp *")]
        public string Onderwerp { get; set; }
        [Required(ErrorMessage ="Inhoud is verplicht")]
        [Display(Name="Inhoud *")]
        public string Inhoud { get; set; }
    }
}
