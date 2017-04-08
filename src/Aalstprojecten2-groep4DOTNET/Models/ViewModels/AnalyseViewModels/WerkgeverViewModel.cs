using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.AspNetCore.Mvc;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class WerkgeverViewModel
    {
        [HiddenInput]
        public int? WerkgeverId { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Straat { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet minstens 1 zijn.")]
        public int Nummer { get; set; }
        [RegularExpression("[a-zA-Z]", ErrorMessage = "{0} moet een letter zijn.")]
        public string Bus { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [RegularExpression("[1-9][0-9]{3}", ErrorMessage = "{0} moet een getal tussen 1000 en 9999 zijn.")]
        public int Postcode { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Gemeente { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet minstens 1 zijn.")]
        [Display(Name = "Aantal werkuren per week")]
        public int AantalWerkuren { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [RegularExpression("^[1-9][0-9]?$|^100$", ErrorMessage = "{0} moet minstens 1 en maximaal 100 zijn.")]
        [Display(Name = "Patronale bijdrage")]
        public int PatronaleBijdrage { get; set; }
        public string LinkNaarLogoPrent { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Naam afdeling")]
        public string NaamAfdeling { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string ContactPersoonNaam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string ContactPersoonVoornaam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress]
        public string ContactPersoonEmail { get; set; }

        public WerkgeverViewModel(Werkgever werkgever)
        {
            WerkgeverId = werkgever.WerkgeverId;
            Naam = werkgever.Naam;
            Straat = werkgever.Straat;
            Nummer = werkgever.Nummer;
            Bus = werkgever.Bus;
            Postcode = werkgever.Postcode;
            Gemeente = werkgever.Gemeente;
            AantalWerkuren = werkgever.AantalWerkuren;
            PatronaleBijdrage = werkgever.PatronaleBijdrage;
            LinkNaarLogoPrent = werkgever.LinkNaarLogoPrent;
            ContactPersoonNaam = werkgever.ContactPersoonNaam;
            ContactPersoonVoornaam = werkgever.ContactPersoonVoornaam;
            ContactPersoonEmail = werkgever.ContactPersoonEmail;
        }

        public WerkgeverViewModel()
        {
            PatronaleBijdrage = 35;
            AantalWerkuren = 38;
        }
    }
}
