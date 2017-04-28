using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Doelgroep
    {
        public int DoelgroepId { get; set; }
        public string DoelgroepText { get; set; }
        public double DoelgroepWaarde { get; set; }
        public double DoelgroepMaxLoon { get; set; }
        public bool IsVerwijdert { get; set; }

        protected Doelgroep()
        {
            
        }

        public Doelgroep(string text, double waarde, double maxLoon, bool isVerwijdert)
        {
            DoelgroepText = text;
            DoelgroepWaarde = waarde;
            DoelgroepMaxLoon = maxLoon;
            IsVerwijdert = isVerwijdert;
        }
    }
}
