using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class AnalyseResultaatOverzichtViewModel
    {
        public int AnalyseId { get; set; }
        public double Kost1 { get; set; }
        public double Kost2 { get; set; }
        public double Kost3 { get; set; }
        public double Kost4 { get; set; }
        public double Kost5 { get; set; }
        public double Kost6 { get; set; }
        public double Kost7 { get; set; }
        public double Kost1Punt1 { get; set; }
        public double SubtotaalKosten { get; set; }
        public double Baat1 { get; set; }
        public double Baat2 { get; set; }
        public double Baat3 { get; set; }
        public double Baat4 { get; set; }
        public double Baat5 { get; set; }
        public double Baat6 { get; set; }
        public double Baat7 { get; set; }
        public double Baat8 { get; set; }
        public double Baat9 { get; set; }
        public double Baat10 { get; set; }
        public double Baat11 { get; set; }
        public double SubtotaalBaten { get; set; }
        public double NettoResultaat { get; set; }

        public AnalyseResultaatOverzichtViewModel(Analyse analyse)
        {
            AnalyseId = analyse.AnalyseId;
            IEnumerable<KostOfBaat> kosten =
                analyse.KostenEnBaten.Where(kost => kost.KostOfBaatEnum == KOBEnum.Kost)
                    .OrderBy(kost => kost.VraagId)
                    .ToList();

            KostOfBaat kob = kosten.SingleOrDefault(k => k.VraagId == 1);
            if (kob != null)
            {
                Kost1 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 2);
            if (kob != null)
            {
                Kost2 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 3);
            if (kob != null)
            {
                Kost3 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 4);
            if (kob != null)
            {
                Kost4 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 5);
            if (kob != null)
            {
                Kost5 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 6);
            if (kob != null)
            {
                Kost6 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 7);
            if (kob != null)
            {
                Kost7 = kob.Resultaat;
            }
            kob = kosten.SingleOrDefault(k => k.VraagId == 8);
            if (kob != null)
            {
                Kost1Punt1 = kob.Resultaat;
            }
            SubtotaalKosten = analyse.KostenResultaat;


            IEnumerable<KostOfBaat> baten =
                analyse.KostenEnBaten.Where(baat => baat.KostOfBaatEnum == KOBEnum.Baat)
                    .OrderBy(baat => baat.VraagId)
                    .ToList();

            kob = baten.SingleOrDefault(b => b.VraagId == 1);
            if (kob != null)
            {
                Baat1 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 2);
            if (kob != null)
            {
                Baat2 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 3);
            if (kob != null)
            {
                Baat3 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 4);
            if (kob != null)
            {
                Baat4 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 5);
            if (kob != null)
            {
                Baat5 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 6);
            if (kob != null)
            {
                Baat6 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 7);
            if (kob != null)
            {
                Baat7 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 8);
            if (kob != null)
            {
                Baat8 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 9);
            if (kob != null)
            {
                Baat9 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 10);
            if (kob != null)
            {
                Baat10 = kob.Resultaat;
            }
            kob = baten.SingleOrDefault(b => b.VraagId == 11);
            if (kob != null)
            {
                Baat11 = kob.Resultaat;
            }
            SubtotaalBaten = analyse.BatenResultaat;
            NettoResultaat = analyse.NettoResultaat;
        }

        public AnalyseResultaatOverzichtViewModel()
        {

        }
    }
}
