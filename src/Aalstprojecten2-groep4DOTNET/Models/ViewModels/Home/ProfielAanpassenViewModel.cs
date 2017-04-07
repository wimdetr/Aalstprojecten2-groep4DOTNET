﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.AccountViewModels;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class ProfielAanpassenViewModel
    {
        public ProfielAanpassenViewModel Origineel { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Bedrijfsnaam")]
        public string NaamBedrijf { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Straat")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Nummer")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet minstens 1 zijn.")]
        public int Nummer { get; set; }

        [Display(Name = "Bus")]
        [RegularExpression("[a-zA-Z]", ErrorMessage = "{0} moet een letter zijn.")]
        public string Bus { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Postcode")]
        [RegularExpression("[1-9][0-9]{3}", ErrorMessage = "{0} moet een getal tussen 1000 en 9999 zijn.")]
        public int Postcode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Gemeente")]
        public string Gemeente { get; set; }

        public ProfielAanpassenViewModel()
        {
            
        }
        public ProfielAanpassenViewModel(ProfielAanpassenViewModel model)
        {
            Origineel = model;
            Email = model.Email;
            Voornaam = model.Voornaam;
            Naam = model.Naam;
            NaamBedrijf = model.NaamBedrijf;
            Straat = model.Straat;
            Nummer = model.Nummer;
            Bus = model.Bus;
            Postcode = model.Postcode;
            Gemeente = model.Gemeente;
        }
    }

}