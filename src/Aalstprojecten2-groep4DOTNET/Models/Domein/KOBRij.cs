using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBRij
    {
        #region Properties
        public int Id { get; set; }
        public ICollection<KOBVak> Vakken { get; set; }
        public double Resultaat { get; set; }
        #endregion

        #region Contructors
        public KOBRij(int id)
        {
            Id = id;
            Vakken = new List<KOBVak>();
            Resultaat = 0;
        }
        #endregion

        #region Methods
        public void VulKOBVakIn(KOBVak vak)
        {
            if (ControleerOfKOBVakMetNummerAlIngevuldIs(vak.Id))
            {
                Vakken.Remove(GeefKOBVakMetNummer(vak.Id));
            }
            Vakken.Add(vak);
        }

        public Boolean ControleerOfKOBVakMetNummerAlIngevuldIs(int nummer)
        {
            return Vakken.Any(v => v.Id == nummer);
        }

        public KOBVak GeefKOBVakMetNummer(int nummer)
        {
            return Vakken.FirstOrDefault(v => v.Id == nummer);
        }
        #endregion
    }
}
