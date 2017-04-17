using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Ocsp;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseKostLijstObjectViewModel
    {
        [Required]
        public int Kost1Id { get; set; }
        public string Functie { get; set; }
        public int AantalUrenPerWeek { get; set; }
        public double BrutoMaandloonFulltime { get; set; }
        public string Doelgroep { get; set; }
        public int VlaamseOndersteuningsPremie { get; set; }
        public int AantalMaandenIBO { get; set; }
        public double TotaleProductiviteitsPremieIBO { get; set; }
    }
}
