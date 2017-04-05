using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class AnalyseOverzichtViewModel
    {
        public string Organisatie { get; set; }
        public string Afdeling { get; set; }
        public string Locatie { get; set; }
        [Display(Name = "Gewijzigd Op")]
        public DateTime GewijzigdOp { get; set; }
        [Display(Name = "Kosten")]
        public double KostenResultaat { get; set; }
        [Display(Name = "Baten")]
        public double BatenResultaat { get; set; }
        public double NettoResultaat { get; set; }

        public AnalyseOverzichtViewModel(Analyse a)
        {
            Organisatie = a.Werkgever.Naam;
            Afdeling = a.Werkgever.NaamAfdeling;
            Locatie = a.Werkgever.Gemeente;
            GewijzigdOp = a.LaatsteAanpasDatum;
            KostenResultaat = a.KostenResultaat;
            BatenResultaat = a.BatenResultaat;
            NettoResultaat = a.NettoResultaat;
        }
    }
}
