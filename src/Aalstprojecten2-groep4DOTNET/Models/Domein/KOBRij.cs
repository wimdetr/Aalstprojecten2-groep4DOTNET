using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBRij
    {
        #region Properties

        public int id  { get; set; }
        public int KOBRijId { get; set; }
        public ICollection<KOBVak> Vakken { get; set; }
        [NotMapped]
        public double Resultaat { get; set; }
        #endregion

        #region Contructors

        public KOBRij()
        {
            
        }
        public KOBRij(KostOfBaat kostOfBaat, int id)
        {
            KOBRijId = id;
            Vakken = new List<KOBVak>();
            Resultaat = 0;
        }
        #endregion

        #region Methods
        public void VulKOBVakIn(KOBVak vak)
        {
            if (ControleerOfKOBVakMetNummerAlIngevuldIs(vak.KOBVakId))
            {
                Vakken.Remove(GeefKOBVakMetNummer(vak.KOBVakId));
            }
            Vakken.Add(vak);
        }

        public Boolean ControleerOfKOBVakMetNummerAlIngevuldIs(int nummer)
        {
            return Vakken.Any(v => v.KOBVakId == nummer);
        }

        public KOBVak GeefKOBVakMetNummer(int nummer)
        {
            return Vakken.FirstOrDefault(v => v.KOBVakId == nummer);
        }
        #endregion
    }
}
