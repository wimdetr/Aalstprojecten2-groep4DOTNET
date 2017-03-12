using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBRij
    {
        public int Id { get; set; }
        public IDictionary<int, KOBVak> Vakken { get; set; }
        public double Resultaat { get; set; }

        public KOBRij(int id)
        {
            Id = id;
            Vakken = new Dictionary<int, KOBVak>();
            Resultaat = 0;
        }

        public void VulKOBVakIn(int nummer, KOBVak vak)
        {
            Vakken[nummer] = vak;
        }

        public Boolean ControleerOfKOBVakMetNummerAlIngevuldIs(int nummer)
        {
            return Vakken.ContainsKey(nummer);
        }

        public KOBVak GeefKOBVakMetNummer(int nummer)
        {
            return Vakken[nummer];
        }
    }
}
