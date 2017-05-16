using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseBaat4ViewModel
    {
        public IList<BeschrijvingBedragViewModel> Lijst { get; set; }
        public int AantalLijnenLijst { get; set; }
        public int VolgendeLijn { get; set; } = -1;
        public string Beschrijving { get; set; }
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaats van een punt")]
        public string Bedrag { get; set; }
        public bool ToonGroep1 { get; set; }
        public bool BevatFout { get; set; } = false;

        public AnalyseBaat4ViewModel()
        {
            AantalLijnenLijst = 0;
            ToonGroep1 = false;
        }
    }
}
