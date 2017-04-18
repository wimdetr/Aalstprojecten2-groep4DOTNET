﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseKost4ViewModel
    {
        public IList<BeschrijvingBedragViewModel> Lijst1 { get; set; }
        public int AantalLijnenLijst1 { get; set; }
        public int VolgendeLijn1 { get; set; } = -1;
        public string Beschrijving1 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Bedrag1 { get; set; }
        public bool ToonGroep1 { get; set; }

        public IList<UrenMaandloonViewModel> Lijst2 { get; set; }
        public int AantalLijnenLijst2 { get; set; }
        public int VolgendeLijn2 { get; set; } = -1;
        [Display(Name = "Uren")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet een positief getal zijn")]
        public string Uren2 { get; set; }
        [Display(Name = "Maandloon")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Maandloon2 { get; set; }
        public bool ToonGroep2 { get; set; }

        public IList<BeschrijvingBedragViewModel> Lijst3 { get; set; }
        public int AantalLijnenLijst3 { get; set; }
        public int VolgendeLijn3 { get; set; } = -1;
        public string Beschrijving3 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Bedrag3 { get; set; }
        public bool ToonGroep3 { get; set; }

        public IList<BeschrijvingBedragViewModel> Lijst4 { get; set; }
        public int AantalLijnenLijst4 { get; set; }
        public int VolgendeLijn4 { get; set; } = -1;
        public string Beschrijving4 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Bedrag4 { get; set; }
        public bool ToonGroep4 { get; set; }

        public AnalyseKost4ViewModel()
        {
            AantalLijnenLijst1 = 0;
            AantalLijnenLijst2 = 0;
            AantalLijnenLijst3 = 0;
            AantalLijnenLijst4 = 0;
            ToonGroep1 = false;
            ToonGroep2 = false;
            ToonGroep3 = false;
            ToonGroep4 = false;
        }
    }
}