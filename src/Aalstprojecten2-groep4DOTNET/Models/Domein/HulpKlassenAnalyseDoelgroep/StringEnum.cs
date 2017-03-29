using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein.HulpKlassenAnalyseDoelgroep
{
    public static class StringEnum
    {
        public static string GeefStringWaarde(Enum waarde)
        {
            string output = null;
            Type type = waarde.GetType();

            FieldInfo fi = type.GetField(waarde.ToString());
            StringWaarde[] attrs = fi.GetCustomAttributes(typeof(StringWaarde), false) as StringWaarde[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Waarde;
            }

            return output;
        }
    }
}
