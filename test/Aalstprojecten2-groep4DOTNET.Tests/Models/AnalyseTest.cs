using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Xunit;

namespace Aalstprojecten2_groep4DOTNET.Tests.Models
{
    public class AnalyseTest
    {
        private Analyse analyse;
        private KostOfBaat kost1;
        private KostOfBaat kost1Punt1;
        private KostOfBaat kost2;
        private KostOfBaat kost3;
        private KostOfBaat kost4;
        private KostOfBaat kost5;
        private KostOfBaat kost6;
        private KostOfBaat kost7;

        private KostOfBaat baat1;
        private KostOfBaat baat2;
        private KostOfBaat baat3;
        private KostOfBaat baat4;
        private KostOfBaat baat5;
        private KostOfBaat baat6;
        private KostOfBaat baat7;
        private KostOfBaat baat8;
        private KostOfBaat baat9;
        private KostOfBaat baat10;
        private KostOfBaat baat11;

        public AnalyseTest()
        {
            analyse = new Analyse();
            analyse.KostenEnBaten = new List<KostOfBaat>();
            kost1 = new KostOfBaat
            {
                KostOfBaatId = 1,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost1Punt1 = new KostOfBaat
            {
                KostOfBaatId = 8,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost2 = new KostOfBaat
            {
                KostOfBaatId = 2,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost3 = new KostOfBaat
            {
                KostOfBaatId = 3,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost4 = new KostOfBaat
            {
                KostOfBaatId = 4,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost5 = new KostOfBaat
            {
                KostOfBaatId = 5,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost6 = new KostOfBaat
            {
                KostOfBaatId = 6,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };

            kost7 = new KostOfBaat
            {
                KostOfBaatId = 7,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Kost
            };


            baat1 = new KostOfBaat
            {
                KostOfBaatId = 1,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat2 = new KostOfBaat
            {
                KostOfBaatId = 2,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat3 = new KostOfBaat
            {
                KostOfBaatId = 3,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat4 = new KostOfBaat
            {
                KostOfBaatId = 4,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat5 = new KostOfBaat
            {
                KostOfBaatId = 5,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat6 = new KostOfBaat
            {
                KostOfBaatId = 6,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat7 = new KostOfBaat
            {
                KostOfBaatId = 7,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat8 = new KostOfBaat
            {
                KostOfBaatId = 8,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat9 = new KostOfBaat
            {
                KostOfBaatId = 9,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat10 = new KostOfBaat
            {
                KostOfBaatId = 10,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            baat11 = new KostOfBaat
            {
                KostOfBaatId = 11,
                Resultaat = 1000,
                KostOfBaatEnum = KOBEnum.Baat
            };

            analyse.SlaKostMetNummerOp(kost1);
            analyse.SlaKostMetNummerOp(kost1Punt1);
            analyse.SlaKostMetNummerOp(kost2);
            analyse.SlaKostMetNummerOp(kost3);
            analyse.SlaKostMetNummerOp(kost4);
            analyse.SlaKostMetNummerOp(kost5);
            analyse.SlaKostMetNummerOp(kost6);
            analyse.SlaKostMetNummerOp(kost7);

            analyse.SlaBaatMetNummerOp(baat1);
            analyse.SlaBaatMetNummerOp(baat2);
            analyse.SlaBaatMetNummerOp(baat3);
            analyse.SlaBaatMetNummerOp(baat4);
            analyse.SlaBaatMetNummerOp(baat5);
            analyse.SlaBaatMetNummerOp(baat6);
            analyse.SlaBaatMetNummerOp(baat7);
            analyse.SlaBaatMetNummerOp(baat8);
            analyse.SlaBaatMetNummerOp(baat9);
            analyse.SlaBaatMetNummerOp(baat10);
            analyse.SlaBaatMetNummerOp(baat11);
        }

        [Fact]
        public void SubtotaalBatenTest()
        {
            analyse.BerekenSubtotaalBaten();
            Assert.Equal(11000, analyse.BatenResultaat);
        }

        [Fact]
        public void SubtotaalKostenTest()
        {
            analyse.BerekenSubtotaalKosten();
            Assert.Equal(8000, analyse.KostenResultaat);
        }

        [Fact]
        public void NettoResultaatTest()
        {
            analyse.BerekenVolledigResultaat();
            Assert.Equal(3000, analyse.NettoResultaat);
        }
    }
}
