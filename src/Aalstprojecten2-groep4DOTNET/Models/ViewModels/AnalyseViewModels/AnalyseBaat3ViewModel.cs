using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseBaat3ViewModel
    {
        public IList<BedragPercentViewModel> Lijst1 { get; set; }
        public int AantalLijnenLijst1 { get; set; }
        public int VolgendeLijn1 { get; set; } = -1;
        [Display(Name = "Percent")]
        [RegularExpression("^[1-9][0-9]?$|^100$", ErrorMessage = "{0} moet minstens 1 en maximaal 100 zijn.")]
        public string Percent1 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Bedrag1 { get; set; }
        public bool ToonGroep1 { get; set; }

        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Baat7 { get; set; }
        [Display(Name = "Bedrag")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Baat2 { get; set; }

        public bool BevatFout { get; set; } = false;

        public AnalyseBaat3ViewModel()
        {
            AantalLijnenLijst1 = 0;
            ToonGroep1 = false;
        }
    }
}
