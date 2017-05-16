using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseBaat2ViewModel
    {
        public IList<BeschrijvingBedragViewModel> Lijst1 { get; set; }
        public int AantalLijnenLijst1 { get; set; }
        public int VolgendeLijn1 { get; set; } = -1;
        public string Beschrijving1 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Bedrag1 { get; set; }
        public bool ToonGroep1 { get; set; }

        public IList<BeschrijvingBedragViewModel> Lijst2 { get; set; }
        public int AantalLijnenLijst2 { get; set; }
        public int VolgendeLijn2 { get; set; } = -1;
        public string Beschrijving2 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Bedrag2 { get; set; }
        public bool ToonGroep2 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Baat8 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Baat10Punt1 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Baat10Punt2 { get; set; }

        public bool BevatFout { get; set; } = false;


        public AnalyseBaat2ViewModel()
        {
            AantalLijnenLijst1 = 0;
            AantalLijnenLijst2 = 0;
            ToonGroep1 = false;
            ToonGroep2 = false;
        }
    }
}
