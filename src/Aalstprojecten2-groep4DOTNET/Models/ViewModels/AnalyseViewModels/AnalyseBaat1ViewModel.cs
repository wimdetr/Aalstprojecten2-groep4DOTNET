using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseBaat1ViewModel
    {
        public IList<UrenMaandloonViewModel> Lijst1 { get; set; }
        public int AantalLijnenLijst1 { get; set; }
        public int VolgendeLijn1 { get; set; } = -1;
        [Display(Name = "Uren")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet een positief getal zijn")]
        public string Uren1 { get; set; }
        [Display(Name = "Maandloon")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string Maandloon1 { get; set; }
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


        public AnalyseBaat1ViewModel()
        {
            AantalLijnenLijst1 = 0;
            AantalLijnenLijst2 = 0;
            ToonGroep1 = false;
            ToonGroep2 = false;
        }
    }
}
