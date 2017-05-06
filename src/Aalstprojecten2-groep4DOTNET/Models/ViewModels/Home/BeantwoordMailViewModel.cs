using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class BeantwoordMailViewModel
    {
        [Required(ErrorMessage ="Admin e-mail is verplicht, contacteer Bart")]
        public string AdminMail { get; set; }
        [Required(ErrorMessage ="Onderwerp is verplicht")]
        [MaxLength(100, ErrorMessage = "Onderwerp mag maximaal 100 karakters bevatten!")]
        [Display(Name="Onderwerp *")]
        public string Onderwerp { get; set; }
        [Required(ErrorMessage ="Inhoud is verplicht")]
        [Display(Name = "Inhoud *")]
        public string Inhoud { get; set; }

        public BeantwoordMailViewModel()
        {
            
        }

        public BeantwoordMailViewModel(InterneMailJobcoach m)
        {
            AdminMail = m.InterneMail.Afzender.Email;
            Onderwerp = "RE: " + m.InterneMail.Onderwerp;
            Inhoud = "\n\n\n" + m.InterneMail.Inhoud;
        }
    }
}
