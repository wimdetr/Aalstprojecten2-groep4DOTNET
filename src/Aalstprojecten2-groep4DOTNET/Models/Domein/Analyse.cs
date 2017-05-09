using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Analyse
    {
        #region Properties
        public string JobCoachEmail { get; set; }
        public int AnalyseId { get; set; }
        public ICollection<KostOfBaat> KostenEnBaten { get; set; }
        public Departement Departement { get; set; }
        public DateTime LaatsteAanpasDatum { get; set; }
        public bool IsGearchiveerd { get; set; }
        public bool IsVerwijderd { get; set; } = false;
        [NotMapped]
        public double KostenResultaat { get; set; }
        [NotMapped]
        public double BatenResultaat { get; set; }
        [NotMapped]
        public double NettoResultaat { get; set; }
        #endregion

        #region Constructor

        public Analyse()
        {
        }
        public Analyse(JobCoach jobCoach, DateTime date)
        {
            JobCoachEmail = jobCoach.Email;
            LaatsteAanpasDatum = date;
            KostenEnBaten = new List<KostOfBaat>();
            IsGearchiveerd = false;
        }
        #endregion

        #region methods
        public bool ControleerOfKostMetNummerAlIngevuldIs(int nummer)
        {
            return KostenEnBaten.Any(k => k.VraagId == nummer && k.KostOfBaatEnum == KOBEnum.Kost);
        }

        public KostOfBaat GeefKostMetNummer(int nummer)
        {
            return KostenEnBaten.FirstOrDefault(k => k.VraagId == nummer && k.KostOfBaatEnum == KOBEnum.Kost);
        }

        public bool ControleerOfBaatMetNummerAlIngevuldIs(int nummer)
        {
            return KostenEnBaten.Any(b => b.VraagId == nummer && b.KostOfBaatEnum == KOBEnum.Baat);
        }

        public KostOfBaat GeefBaatMetNummer(int nummer)
        {
            return KostenEnBaten.FirstOrDefault(b => b.VraagId == nummer && b.KostOfBaatEnum == KOBEnum.Baat);
        }

        public void SlaKostMetNummerOp(KostOfBaat kost)
        {
            if (ControleerOfKostMetNummerAlIngevuldIs(kost.VraagId))
            {
                KostenEnBaten.Remove(GeefKostMetNummer(kost.VraagId));
            }
            KostenEnBaten.Add(kost);
        }

        public void SlaBaatMetNummerOp(KostOfBaat baat)
        {
            if (ControleerOfBaatMetNummerAlIngevuldIs(baat.VraagId))
            {
                KostenEnBaten.Remove(GeefBaatMetNummer(baat.VraagId));
            }
            KostenEnBaten.Add(baat);
        }

        public void BerekenSubtotaalKosten()
        {
            KostenResultaat = KostenEnBaten.Where(kob => kob.KostOfBaatEnum == KOBEnum.Kost).Sum(kob => kob.Resultaat);
            
        }

        public void BerekenSubtotaalBaten()
        {
            BatenResultaat = KostenEnBaten.Where(kob => kob.KostOfBaatEnum == KOBEnum.Baat).Sum(kob => kob.Resultaat);
            
        }

        public void BerekenResultaat()
        {
            NettoResultaat = BatenResultaat - KostenResultaat;
        }

        public void BerekenVolledigResultaat()
        {
            BerekenSubtotaalBaten();
            BerekenSubtotaalKosten();
            BerekenResultaat();
        }

        public void VernieuwDatum()
        {
            LaatsteAanpasDatum = DateTime.Now;
        }
        #endregion
    }
}
