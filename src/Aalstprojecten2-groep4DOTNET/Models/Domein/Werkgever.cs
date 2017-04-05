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
        public int AnalyseId { get; set; }
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
        public ContactPersoon ContactPersoon { get; set; }
        public string NaamAfdeling { get; set; }
        #endregion

        #region Constructor

        public Werkgever()
        {
            
        }
        public Werkgever(Analyse a, string naam, int postcode, string gemeente, string naamAfdeling)
        {
            AnalyseId = a.AnalyseId;
            JobCoachEmail = a.JobCoachEmail;
            Naam = naam;
            Postcode = postcode;
            Gemeente = gemeente;
            NaamAfdeling = naamAfdeling;
            PatronaleBijdrage = _defaultPatronaleBijdrage;
            AantalWerkuren = _defaultAantalWerkuren;
        }
        #endregion 
    }
}
