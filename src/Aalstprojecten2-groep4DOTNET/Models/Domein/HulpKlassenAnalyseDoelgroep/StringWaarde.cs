using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein.HulpKlassenAnalyseDoelgroep
{
    public class StringWaarde : System.Attribute
    {
        private readonly string _waarde;

        public StringWaarde(string warde)
        {
            _waarde = _waarde;
        }

        public String Waarde
        {
            get { return _waarde;}
        }
    }
}
