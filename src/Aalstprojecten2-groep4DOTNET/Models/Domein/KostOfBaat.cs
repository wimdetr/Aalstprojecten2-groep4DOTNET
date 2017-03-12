using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KostOfBaat
    {
        public int Id { get; set; }
        public IDictionary<int, KOBRij> Rijen { get; set; }
        public KOBEnum KostOfBaatEnum { get; set; }
        public Formule Formule { get; set; }
        public double Resultaat { get; set; }

        public KostOfBaat(int id, KOBEnum kobEnum, Formule formule)
        {
            Id = id;
            KostOfBaatEnum = kobEnum;
            Resultaat = 0;
            Rijen = new Dictionary<int, KOBRij>();
            Formule = formule;
        }

        public void VulKOBRijIn(int nummer, KOBRij rij)
        {
            Rijen[nummer] = rij;
        }

        public KOBRij GeefKOBRijMetNummer(int nummer)
        {
            return Rijen[nummer];
        }

        public Boolean ControleerOfKOBRijMetNummerAlIngevuldIs(int nummer)
        {
            return Rijen.ContainsKey(nummer);
        }

        public void BerekenResultaat()
        {
            Resultaat = Rijen.Values.Sum(r => r.Resultaat);
        }
    }
}
