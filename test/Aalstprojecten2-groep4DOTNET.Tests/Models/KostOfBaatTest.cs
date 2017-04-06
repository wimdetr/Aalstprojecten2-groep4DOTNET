using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Moq;
using Xunit;

namespace Aalstprojecten2_groep4DOTNET.Tests.Models
{
    public class KostOfBaatTest
    {
        private KostOfBaat kostOfBaat;
        private KOBRij rij1;
        private KOBRij rij2;
        private KOBRij rij3;

        public KostOfBaatTest()
        {
            kostOfBaat = new KostOfBaat();
            kostOfBaat.Rijen = new List<KOBRij>();
            rij1 = new KOBRij();
            rij1.KOBRijId = 1;
            rij2 = new KOBRij();
            rij2.KOBRijId = 2;
            rij3 = new KOBRij();
            rij3.KOBRijId = 3;
            kostOfBaat.VulKOBRijIn(rij1);
            kostOfBaat.VulKOBRijIn(rij2);
            kostOfBaat.VulKOBRijIn(rij3);
        }

        [Fact]
        public void Kost1()
        {
            kostOfBaat.Formule = Formule.FormuleKost1;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(60600, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost1Punt1()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost2()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost3()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost4()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost5()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost6()
        {
            kostOfBaat.Formule = Formule.FormuleKost6;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Kost7()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat1()
        {
            kostOfBaat.Formule = Formule.FormuleBaat1;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat2()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak1;
            rij1.Resultaat = 5000;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5000, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat3()
        {
            kostOfBaat.Formule = Formule.FormuleBaat3En4;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat4()
        {
            kostOfBaat.Formule = Formule.FormuleBaat3En4;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat5()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat6()
        {
            kostOfBaat.Formule = Formule.FormuleVermenigvuldigVak1En2;
            rij1.Resultaat = 1000;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(1000, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat7()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak1;
            rij1.Resultaat = 5000;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5000, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat8()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak1;
            rij1.Resultaat = 5000;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5000, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat9()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat10()
        {
            kostOfBaat.Formule = Formule.FormuleSomVak1En2;
            rij1.Resultaat = 12500;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(12500, kostOfBaat.Resultaat);
        }

        [Fact]
        public void Baat11()
        {
            kostOfBaat.Formule = Formule.FormuleGeefVak2;
            rij1.Resultaat = 1000;
            rij2.Resultaat = 250;
            rij3.Resultaat = 3800;

            kostOfBaat.BerekenResultaat();
            Assert.Equal(5050, kostOfBaat.Resultaat);
        }
    }
}
