using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Werkgever
    {
        private readonly int _defaultPatronaleBijdrage = 35;
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public int AantalWerkuren { get; set; }
        public int PatronaleBijdrage { get; set; }
        public string LinkNaarLogoPrent { get; set; }
        public ContactPersoon ContactPersoon { get; set; }
        public string NaamAfdeling { get; set; }

        public Werkgever(string naam, int postcode, string gemeente)
        {
            Naam = naam;
            Postcode = postcode;
            Gemeente = gemeente;
            PatronaleBijdrage = _defaultPatronaleBijdrage;
        }
    }
}
