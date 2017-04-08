using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Xunit;

namespace Aalstprojecten2_groep4DOTNET.Tests.Models
{
    public class ResultaatTest
    {
        private Resultaat resultaat;
        private Analyse analyse;
        private Werkgever werkgever;

        public ResultaatTest()
        {
            resultaat = new Resultaat();
            analyse = new Analyse();
            analyse.KostenEnBaten = new List<KostOfBaat>();
            werkgever = new Werkgever();
            werkgever.AantalWerkuren = 38;
            werkgever.PatronaleBijdrage = 35;
            analyse.Werkgever = werkgever;
        }

        #region Kost1
        [Fact]
        public void Kost1_25Laaggeschoold_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Laaggeschoold_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Laaggeschoold_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Laaggeschoold_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Middengeschoold_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Middengeschoold_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Middengeschoold_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_25Middengeschoold_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Tussen55en60_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Tussen55en60_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Tussen55en60_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Tussen55en60_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_60enOuder_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_60enOuder_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_60enOuder_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_60enOuder_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Ander_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Ander_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Ander_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }

        [Fact]
        public void Kost1_Ander_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1243.42);
        }
        #endregion

        #region Baat1

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);
            

            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 8000.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 9234.32);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 10467.79);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 12934.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 8000.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 9234.32);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 10467.79);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "3500");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 12934.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 11172.63);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 12934.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 14696.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 18221.05);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 11172.63);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 12934.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 14696.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 18221.05);
        }

        [Fact]
        public void Baat1Bij_Kost1_Ander_premie40()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 11172.63);
        }

        [Fact]
        public void Baat1Bij_Kost1_Ander_premie30()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 12934.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_Ander_premie20()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 14696.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_Ander_premie0()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "5000");
            KOBVak vak4 = new KOBVak(rij, 4, "ander");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 18221.05);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie40_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4222.11);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie30_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4825.79);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie20_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5429.47);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Laaggeschoold_premie0_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar laaggeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 6636.84);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie40_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4437.47);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie30_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5077.05);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie20_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5716.63);
        }

        [Fact]
        public void Baat1Bij_Kost1_25Middengeschoold_premie0_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn minder dan 25 jaar middengeschoold");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 6995.79);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie40_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4378.74);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie30_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5008.53);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie20_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5638.32);
        }

        [Fact]
        public void Baat1Bij_Kost1_Tussen55en60_premie0_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of gelijk aan 55 of minder dan 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 6897.89);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie40_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "40");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4241.68);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie30_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "30");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 4848.63);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie20_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "20");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 5455.58);
        }

        [Fact]
        public void Baat1Bij_Kost1_60enOuder_premie0_LaagMaandloon()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "programmeur");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            KOBVak vak3 = new KOBVak(rij, 3, "2000");
            KOBVak vak4 = new KOBVak(rij, 4, "wn meer of evenveel als 60 jaar");
            KOBVak vak5 = new KOBVak(rij, 5, "0");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            rij.VulKOBVakIn(vak3);
            rij.VulKOBVakIn(vak4);
            rij.VulKOBVakIn(vak5);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);


            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 6669.47);
        }

        [Fact]
        public void Baat1Bij_Kost1_NietIngevuld()
        {
            KostOfBaat baat = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
            KOBRij baatRij = new KOBRij(baat, 1);
            KOBVak baatVak1 = new KOBVak(baatRij, 1, "4");
            KOBVak baatVak2 = new KOBVak(baatRij, 2, "600");
            baatRij.VulKOBVakIn(baatVak1);
            baatRij.VulKOBVakIn(baatVak2);
            baat.VulKOBRijIn(baatRij);
            analyse.SlaBaatMetNummerOp(baat);
            resultaat.GeefParametersDoor(baat, analyse);
            resultaat.BerekenEnSetResultaat(baatRij);


            Assert.Equal(Math.Round(baatRij.Resultaat, 2), 600);
        }

        #endregion

        #region GeefVak2

        [Fact]
        public void GeefVak2()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 2, KOBEnum.Kost, Formule.FormuleGeefVak2);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "ruiten wassen");
            KOBVak vak2 = new KOBVak(rij, 2, "600");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 600);
        }

        #endregion

        #region Kost6

        [Fact]
        public void Kost6()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 6, KOBEnum.Kost, Formule.FormuleKost6);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "150");
            KOBVak vak2 = new KOBVak(rij, 2, "3000");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 3996.71);
        }

        #endregion

        #region GeefVak1

        [Fact]
        public void GeefVak1()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 2, KOBEnum.Baat, Formule.FormuleGeefVak1);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "5000");
            rij.VulKOBVakIn(vak1);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 5000);
        }

        #endregion

        #region Baat3En4

        [Fact]
        public void Baat3()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 3, KOBEnum.Baat, Formule.FormuleBaat3En4);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "10");
            KOBVak vak2 = new KOBVak(rij, 2, "3000");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 14835.79);
        }

        [Fact]
        public void Baat4()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 4, KOBEnum.Baat, Formule.FormuleBaat3En4);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "10");
            KOBVak vak2 = new KOBVak(rij, 2, "4000");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 19781.05);
        }

        #endregion

        #region VermenigvuldigVak1En2

        [Fact]
        public void VermenigvuldigVak1En2()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 6, KOBEnum.Baat, Formule.FormuleVermenigvuldigVak1En2);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "10000");
            KOBVak vak2 = new KOBVak(rij, 2, "10");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 1000);
        }

        #endregion

        #region SomVak1En2

        [Fact]
        public void SomVak1En2()
        {
            KostOfBaat kost = new KostOfBaat(analyse, 10, KOBEnum.Baat, Formule.FormuleSomVak1En2);
            KOBRij rij = new KOBRij(kost, 1);
            KOBVak vak1 = new KOBVak(rij, 1, "5000");
            KOBVak vak2 = new KOBVak(rij, 2, "7500");
            rij.VulKOBVakIn(vak1);
            rij.VulKOBVakIn(vak2);
            kost.VulKOBRijIn(rij);
            analyse.SlaKostMetNummerOp(kost);
            resultaat.GeefParametersDoor(kost, analyse);
            resultaat.BerekenEnSetResultaat(rij);
            Assert.Equal(Math.Round(rij.Resultaat, 2), 12500);
        }

        #endregion
    }
}
