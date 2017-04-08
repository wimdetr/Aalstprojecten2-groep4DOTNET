using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class BestaandeWerkgeverInfoViewModel
    {
        public int? WerkgeverId { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public string NaamAfdeling { get; set; }
        public string ContactPersoonNaam { get; set; }

        public BestaandeWerkgeverInfoViewModel(Werkgever werkgever)
        {
            WerkgeverId = werkgever.WerkgeverId;
            Naam = werkgever.Naam;
            Straat = werkgever.Straat;
            Postcode = werkgever.Postcode;
            Gemeente = werkgever.Gemeente;
            NaamAfdeling = werkgever.NaamAfdeling;
            ContactPersoonNaam = werkgever.ContactPersoonNaam + " " + werkgever.ContactPersoonVoornaam;
        }
    }
}
