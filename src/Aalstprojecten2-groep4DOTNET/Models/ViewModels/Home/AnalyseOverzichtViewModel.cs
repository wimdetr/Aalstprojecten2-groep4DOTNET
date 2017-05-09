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
        public int Id { get; set; }
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
        public double BatenBalk { get; set; }
        public double KostenBalk { get; set; }

        public AnalyseOverzichtViewModel(Analyse a)
        {
            Id = a.AnalyseId;
            Organisatie = a.Departement.Werkgever.Naam;
            Afdeling = a.Departement.Naam;
            Locatie = a.Departement.Gemeente;
            GewijzigdOp = a.LaatsteAanpasDatum;
            KostenResultaat = a.KostenResultaat;
            BatenResultaat = a.BatenResultaat;
            NettoResultaat = a.NettoResultaat;


            if (BatenResultaat == KostenResultaat)
            {
                BatenBalk = 50;
                KostenBalk = 50;
            }
            else
            {
                KostenBalk = Math.Round(KostenResultaat/(BatenResultaat + KostenResultaat)*100);
                BatenBalk = 100 - Math.Round(KostenResultaat/(BatenResultaat + KostenResultaat)*100);
            }
        }
    }
}
