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
        
        #endregion

        #region Properties
        public string JobCoachEmail { get; set; }
        public int WerkgeverId { get; set; }
        public string Naam { get; set; }
        public int PatronaleBijdrage { get; set; }
        public string LinkNaarLogoPrent { get; set; }
        #endregion

        #region Constructor

        public Werkgever()
        {
            PatronaleBijdrage = _defaultPatronaleBijdrage;
        }

        public Werkgever(string jcmail, string naam) : this()
        {
            JobCoachEmail = jcmail;
            Naam = naam;
        }
        #endregion
    }
}

