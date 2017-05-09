using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Departement
    {
        private readonly int _defaultAantalWerkuren = 38;

        public int? AnalyseId { get; set; }
        public int? DepartementId { get; set; }
        public Werkgever Werkgever { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public int Nummer { get; set; }
        public string Bus { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public int AantalWerkuren { get; set; }
        public string ContactPersoonNaam { get; set; }
        public string ContactPersoonVoornaam { get; set; }
        public string ContactPersoonEmail { get; set; }

        public Departement()
        {
            AantalWerkuren = _defaultAantalWerkuren;
        }

        public Departement(Analyse a, Werkgever w, string naam, string straat, int nummer, string bus, int postcode, string gemeente) : this()
        {
            AnalyseId = a.AnalyseId;
            Werkgever = w;
            Naam = naam;
            Straat = straat;
            Nummer = nummer;
            Bus = bus;
            Postcode = postcode;
            Gemeente = gemeente;
        }
    }
}
