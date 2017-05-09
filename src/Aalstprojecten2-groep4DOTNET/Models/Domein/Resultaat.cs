using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Resultaat
    {
        #region Properties
        public KostOfBaat KostOfBaat { get; private set; }
        public Analyse Analyse { get; private set; }
        public IEnumerable<Doelgroep> Doelgroepen { get; set; }
        #endregion

        #region Contructor
        public Resultaat(IEnumerable<Doelgroep> doelgroepen)
        {
            Doelgroepen = doelgroepen;
        }
        #endregion

        #region Methods
        public void GeefParametersDoor(KostOfBaat kob, Analyse a)
        {
            KostOfBaat = kob;
            Analyse = a;
        }

        public void BerekenResultaatVanAnalyse(Analyse a)
        {
            foreach (KostOfBaat kob in a.KostenEnBaten)
            {
                GeefParametersDoor(kob, a);
                foreach (KOBRij rij in kob.Rijen)
                {
                    BerekenEnSetResultaat(rij);
                }
                kob.BerekenResultaat();
            }
            a.BerekenVolledigResultaat();
        }

        public void BerekenEnSetResultaat(KOBRij mijnRij)
        {
            int aantalWerkuren = Analyse.Departement.AantalWerkuren;
            switch (KostOfBaat.Formule)
            {
                    case Formule.FormuleBaat1:
                    if (aantalWerkuren == 0)
                    {
                        //todo, divide by zero exception maken en throwen
                    }
                    KOBRij kostRij = GeefRijVanKost(KostOfBaat.VraagId, mijnRij.KOBRijId);
                    if (kostRij == null)
                    {
                        mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    }
                    else
                    {
                        HerberekenKost1(kostRij, aantalWerkuren);
                        double tussenWaarde;
                        string dataVak4 = kostRij.GeefKOBVakMetNummer(4).Data;
                        double dataVak3 = kostRij.GeefKOBVakMetNummer(3).GeefDataAlsDouble();
                        double dataVak2 = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                        Doelgroep doelgroep = Doelgroepen.SingleOrDefault(d => d.DoelgroepText.Equals(dataVak4));
                        if (dataVak3 < doelgroep.DoelgroepMaxLoon)
                        {
                            tussenWaarde = doelgroep.DoelgroepWaarde/aantalWerkuren*dataVak2/4;
                        }
                        else
                        {
                            tussenWaarde = 0;
                        }
                        double tussenWaarde2 = (kostRij.Resultaat - tussenWaarde)*
                                               kostRij.GeefKOBVakMetNummer(5).GeefDataAlsDouble()/100;
                        mijnRij.Resultaat = (kostRij.Resultaat - (tussenWaarde2 + tussenWaarde))*
                                            (13.92 - mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()) +
                                            mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    }
                    break;
                    case Formule.FormuleKost1:
                    if (aantalWerkuren == 0)
                    {
                        //todo, divide by zero exception maken en throwen
                    }
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(3).GeefDataAlsDouble()/aantalWerkuren*
                                          mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()*
                                         (1 + (double)Analyse.Departement.Werkgever.PatronaleBijdrage/100);
                    break;
                    case Formule.FormuleGeefVak2:
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    break;
                    case Formule.FormuleKost6:
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()/152*
                                        mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()*
                                        (1 + (double)Analyse.Departement.Werkgever.PatronaleBijdrage/100);
                    break;
                    case Formule.FormuleGeefVak1:
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble();
                    break;
                    case Formule.FormuleBaat3En4:
                    if (mijnRij.GeefKOBVakMetNummer(1).Data.Trim().Equals(""))
                    {
                        mijnRij.Resultaat = 0;
                    }
                    else
                    {
                        mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()/aantalWerkuren*
                                            mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()*
                                            (1 + (double)Analyse.Departement.Werkgever.PatronaleBijdrage/100)*13.92;
                    }
                    break;
                    case Formule.FormuleSomVak1En2:
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble() +
                                        mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    break;
                    case Formule.FormuleVermenigvuldigVak1En2:
                    mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble() *
                                        mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()/100;
                    break;
                default:
                    mijnRij.Resultaat = 0;
                    break;
            }
        }

        private void HerberekenKost1(KOBRij mijnRij, int aantalWerkuren)
        {
            if (aantalWerkuren == 0)
            {
                //todo, divide by zero exception maken en throwen
            }
            mijnRij.Resultaat = mijnRij.GeefKOBVakMetNummer(3).GeefDataAlsDouble() / aantalWerkuren *
                                  mijnRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble() *
                                 (1 + (double)Analyse.Departement.Werkgever.PatronaleBijdrage / 100);
        }

        public KOBRij GeefRijVanKost(int kostId, int rijId)
        {
            if (Analyse.ControleerOfKostMetNummerAlIngevuldIs(kostId))
            {
                KostOfBaat tempKOB = Analyse.GeefKostMetNummer(kostId);
                if (tempKOB.ControleerOfKOBRijMetNummerAlIngevuldIs(rijId))
                {
                    return tempKOB.GeefKOBRijMetNummer(rijId);
                }
                
            }
            return null;
        }
        #endregion 
    }
}
