﻿using System;
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
        public Werkgever Werkgever { get; set; }
        public DateTime LaatsteAanpasDatum { get; set; }
        [NotMapped]
        public double KostenResultaat { get; set; }
        [NotMapped]
        public double BatenResultaat { get; set; }
        [NotMapped]
        public double NettoResultaat { get; set; }
        #endregion

        #region Constructor
        public Analyse() { }
        public Analyse(JobCoach jobCoach, int id, DateTime date)
        {
            JobCoachEmail = jobCoach.Email;
            AnalyseId = id;
            LaatsteAanpasDatum = date;
            KostenEnBaten = new List<KostOfBaat>();
        }
        #endregion

        #region methods
        public Boolean ControleerOfKostMetNummerAlIngevuldIs(int nummer)
        {
            return KostenEnBaten.Any(k => k.KostOfBaatId == nummer && k.KostOfBaatEnum == KOBEnum.Kost);
        }

        public KostOfBaat GeefKostMetNummer(int nummer)
        {
            return KostenEnBaten.FirstOrDefault(k => k.KostOfBaatId == nummer && k.KostOfBaatEnum == KOBEnum.Kost);
        }

        public Boolean ControleerOfBaatMetNummerAlIngevuldIs(int nummer)
        {
            return KostenEnBaten.Any(b => b.KostOfBaatId == nummer && b.KostOfBaatEnum == KOBEnum.Baat);
        }

        public KostOfBaat GeefBaatMetNummer(int nummer)
        {
            return KostenEnBaten.FirstOrDefault(b => b.KostOfBaatId == nummer && b.KostOfBaatEnum == KOBEnum.Baat);
        }

        public void SlaKostMetNummerOp(KostOfBaat kost)
        {
            if (ControleerOfKostMetNummerAlIngevuldIs(kost.KostOfBaatId))
            {
                KostenEnBaten.Remove(GeefKostMetNummer(kost.KostOfBaatId));
            }
            KostenEnBaten.Add(kost);
        }

        public void SlaBaatMetNummerOp(KostOfBaat baat)
        {
            if (ControleerOfBaatMetNummerAlIngevuldIs(baat.KostOfBaatId))
            {
                KostenEnBaten.Remove(GeefBaatMetNummer(baat.KostOfBaatId));
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
