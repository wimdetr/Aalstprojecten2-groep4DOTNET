using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Werkgever
    {
        #region Fields
        private readonly int _defaultPatronaleBijdrage = 35;
        private readonly int _defaultAantalWerkuren = 38;
        #endregion

        #region Properties
        public int? AnalyseId { get; set; }
        public string JobCoachEmail { get; set; }
        public int? WerkgeverId { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public int Nummer { get; set; }
        public string Bus { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public int AantalWerkuren { get; set; }
        public int PatronaleBijdrage { get; set; }
        public string LinkNaarLogoPrent { get; set; }
        public string NaamAfdeling { get; set; }
        public string ContactPersoonNaam { get; set; }
        public string ContactPersoonVoornaam { get; set; }
        public string ContactPersoonEmail { get; set; }
        #endregion

        #region Constructor

        public Werkgever()
        {
            PatronaleBijdrage = _defaultPatronaleBijdrage;
            AantalWerkuren = _defaultAantalWerkuren;
        }

        public Werkgever(Analyse a, string naam, int postcode, string gemeente, string naamAfdeling) : this()
        {
            AnalyseId = a.AnalyseId;
            JobCoachEmail = a.JobCoachEmail;
            Naam = naam;
            Postcode = postcode;
            Gemeente = gemeente;
            NaamAfdeling = naamAfdeling;

        }
        #endregion

        #region Methodes

        public override bool Equals(object obj)
        {
            return Naam.Equals(((Werkgever) obj).Naam) && Straat.Equals(((Werkgever) obj).Straat) &&
                   Postcode == ((Werkgever) obj).Postcode && Gemeente.Equals(((Werkgever) obj).Gemeente) &&
                   ContactPersoonNaam.Equals(((Werkgever)obj).ContactPersoonNaam) &&
                   ContactPersoonVoornaam.Equals(((Werkgever)obj).ContactPersoonVoornaam) &&
                   ContactPersoonEmail.Equals(((Werkgever) obj).ContactPersoonEmail);
        }

        #endregion
    }
}

