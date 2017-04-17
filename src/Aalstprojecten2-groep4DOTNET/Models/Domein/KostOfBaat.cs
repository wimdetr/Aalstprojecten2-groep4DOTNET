using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KostOfBaat
    {
        #region Properties
        public int AnalyseId { get; set; }
        public int KostOfBaatId { get; set; }
        public ICollection<KOBRij> Rijen { get; set; }
        public KOBEnum KostOfBaatEnum { get; set; }
        public Formule Formule { get; set; }
        [NotMapped]
        public double Resultaat { get; set; }
        #endregion

        #region Constructor

        public KostOfBaat()
        {
            
        }
        public KostOfBaat(Analyse a, int id, KOBEnum kobEnum, Formule formule)
        {
            AnalyseId = a.AnalyseId;
            KostOfBaatId = id;
            KostOfBaatEnum = kobEnum;
            Resultaat = 0;
            Rijen = new List<KOBRij>();
            Formule = formule;
        }
        #endregion

        #region Methods
        public void VulKOBRijIn(KOBRij rij)
        {
            if (ControleerOfKOBRijMetNummerAlIngevuldIs(rij.KOBRijId))
            {
                Rijen.Remove(GeefKOBRijMetNummer(rij.KOBRijId));
            }
            Rijen.Add(rij);
        }

        public KOBRij GeefKOBRijMetNummer(int nummer)
        {
            return Rijen.FirstOrDefault(r => r.KOBRijId == nummer);
        }

        public bool ControleerOfKOBRijMetNummerAlIngevuldIs(int nummer)
        {
            return Rijen.Any(r => r.KOBRijId == nummer);
        }

        public void BerekenResultaat()
        {
            Resultaat = Rijen.Sum(r => r.Resultaat);
            if (Formule == Formule.FormuleKost1)
            {
                Resultaat *= 12;
            }
        }

        public void VerwijderKOBRij(KOBRij rij)
        {
            if(rij != null && Rijen.Contains(rij))
            {
                Rijen.Remove(rij);
            }
        }


        #endregion
    }
}
