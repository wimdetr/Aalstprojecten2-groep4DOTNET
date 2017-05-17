using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [Display(Name = "Voornaam *")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        [Display(Name = "Naam *")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "E-mail moet een geldig e-mail adres zijn.")]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bedrijfsnaam is verplicht.")]
        [Display(Name = "Bedrijfsnaam *")]
        public string NaamBedrijf { get; set; }

        [Required(ErrorMessage = "Straat is verplicht.")]
        [Display(Name = "Straat *")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "Nummer is verplicht.")]
        [Display(Name = "Nummer *")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Nummer moet minstens 1 zijn.")]
        public int Nummer { get; set; }

        [Display(Name = "Bus")]
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Bus moet een letter zijn.")]
        public string Bus { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht.")]
        [Display(Name = "Postcode *")]
        [RegularExpression("[1-9][0-9]{3}", ErrorMessage = "Postcode moet een getal tussen 1000 en 9999 zijn.")]
        public int Postcode { get; set; }

        [Required(ErrorMessage = "Gemeente is verplicht.")]
        [Display(Name = "Gemeente *")]
        public string Gemeente { get; set; }

    }
}
