using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KostOfBaat
    {
        #region Properties
        public int Id { get; set; }
        public ICollection<KOBRij> Rijen { get; set; }
        public KOBEnum KostOfBaatEnum { get; set; }
        public Formule Formule { get; set; }
        public double Resultaat { get; set; }
        #endregion

        #region Constructor
        public KostOfBaat(int id, KOBEnum kobEnum, Formule formule)
        {
            Id = id;
            KostOfBaatEnum = kobEnum;
            Resultaat = 0;
            Rijen = new List<KOBRij>();
            Formule = formule;
        }
        #endregion

        #region Methods
        public void VulKOBRijIn(KOBRij rij)
        {
            if (ControleerOfKOBRijMetNummerAlIngevuldIs(rij.Id))
            {
                Rijen.Remove(GeefKOBRijMetNummer(rij.Id));
            }
            Rijen.Add(rij);
        }

        public KOBRij GeefKOBRijMetNummer(int nummer)
        {
            return Rijen.FirstOrDefault(r => r.Id == nummer);
        }

        public Boolean ControleerOfKOBRijMetNummerAlIngevuldIs(int nummer)
        {
            return Rijen.Any(r => r.Id == nummer);
        }

        public void BerekenResultaat()
        {
            Resultaat = Rijen.Sum(r => r.Resultaat);
        }
        #endregion
    }
}
