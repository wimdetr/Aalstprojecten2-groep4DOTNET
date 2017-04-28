using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseKostViewModel
    {
        public IList<AnalyseKostLijstObjectViewModel> LijnenKost1 { get; set; }
        public int AantalLijnenKost1 { get; set; }
        public int VolgendeLijn { get; set; } = -1;

        public string Functie { get; set; }
        [Display(Name = "Aantal uren per week")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet een positief getal zijn")]
        public string AantalUrenPerWeek { get; set; }
        [Display(Name = "Bruto maandloon fulltime")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string BrutoMaandloonFulltime { get; set; }
        public string Doelgroep { get; set; }
        [Display(Name = "Vlaamse ondersteuningspremie")]
        public string VlaamseOndersteuningsPremie { get; set; }
        [Display(Name = "Aantal maanden IBO")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "{0} moet een positief getal zijn")]
        public string AantalMaandenIBO { get; set; }
        [Display(Name = "Totale productiviteits premie IBO")]
        [RegularExpression("[1-9][0-9]*([,][0-9]+)?", ErrorMessage = "{0} moet een positief getal zijn, gebruik een komma in plaat van een punt")]
        public string TotaleProductiviteitsPremieIBO { get; set; }
        public IEnumerable<string> Doelgroepen { get; set; }

        public bool ToonGroep1 { get; set; } = false;
        public bool BevatFout { get; set; } = false;
    }

    
}
