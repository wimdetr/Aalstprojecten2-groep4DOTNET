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
        public int? DepartementId { get; set; }
        public string Titel { get; set; }
        public bool Aanpassen { get; set; } = false;
        [Display(Name ="Naam *")]
        [Required(ErrorMessage = "Naam is verplicht.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Straat is verplicht.")]
        [Display(Name = "Straat *")]
        public string Straat { get; set; }
        [Required(ErrorMessage = "Nummer is verplicht.")]
        [Display(Name = "Nummer *")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Nummer moet minstens 1 zijn.")]
        public int Nummer { get; set; }
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Bus moet een letter zijn.")]
        public string Bus { get; set; }
        [Required(ErrorMessage = "Postcode is verplicht.")]
        [Display(Name = "Postcode *")]
        [RegularExpression("[1-9][0-9]{3}", ErrorMessage = "Postcode moet een getal tussen 1000 en 9999 zijn.")]
        public int Postcode { get; set; }
        [Required(ErrorMessage = "Gemeente is verplicht.")]
        [Display(Name = "Gemeente *")]
        public string Gemeente { get; set; }
        [Required(ErrorMessage = "AantalWerkuren is verplicht.")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "AantalWerkuren moet minstens 1 zijn.")]
        [Display(Name = "Uren per week *")]
        public int AantalWerkuren { get; set; }
        [Required(ErrorMessage = "Patronale bijdrage is verplicht.")]
        [RegularExpression("^[1-9][0-9]?$|^100$", ErrorMessage = "Patronale bijdrage moet minstens 1 en maximaal 100 zijn.")]
        [Display(Name = "Patronale bijdrage *")]
        public int PatronaleBijdrage { get; set; }
        public string LinkNaarLogoPrent { get; set; }
        [Required(ErrorMessage = "Naam afdeling is verplicht.")]
        [Display(Name = "Naam afdeling *")]
        public string NaamAfdeling { get; set; }
        [Display(Name = "Contactpersoon Naam")]
        public string ContactPersoonNaam { get; set; }
        [Display(Name = "Contactpersoon Voornaam")]
        public string ContactPersoonVoornaam { get; set; }
        [Display(Name = "Contactpersoon e-mail")]
        [EmailAddress]
        public string ContactPersoonEmail { get; set; }

        public WerkgeverViewModel(Departement departement)
        {
            DepartementId = departement.DepartementId;
            Naam = departement.Werkgever.Naam;
            Straat = departement.Straat;
            Nummer = departement.Nummer;
            Bus = departement.Bus;
            Postcode = departement.Postcode;
            Gemeente = departement.Gemeente;
            AantalWerkuren = departement.AantalWerkuren;
            PatronaleBijdrage = departement.Werkgever.PatronaleBijdrage;
            LinkNaarLogoPrent = departement.Werkgever.LinkNaarLogoPrent;
            ContactPersoonNaam = departement.ContactPersoonNaam;
            ContactPersoonVoornaam = departement.ContactPersoonVoornaam;
            ContactPersoonEmail = departement.ContactPersoonEmail;
        }

        public WerkgeverViewModel(Werkgever werkgever)
        {
            Naam = werkgever.Naam;
            PatronaleBijdrage = werkgever.PatronaleBijdrage;
            LinkNaarLogoPrent = werkgever.LinkNaarLogoPrent;
            AantalWerkuren = 38;
        }

        public WerkgeverViewModel()
        {
            PatronaleBijdrage = 35;
            AantalWerkuren = 38;
        }
    }
}
