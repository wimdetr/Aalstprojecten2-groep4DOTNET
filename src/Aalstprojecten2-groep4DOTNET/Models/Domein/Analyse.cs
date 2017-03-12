using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Analyse
    {
        public int Id { get; set; }
        public IDictionary<int, KostOfBaat> Kosten { get; set; }
        
        public IDictionary<int, KostOfBaat> Baten { get; set; }
        public Werkgever Werkgever { get; set; }
        public DateTime LaatsteAanpasDatum { get; set; }

        public Analyse(int id, DateTime date)
        {
            Id = id;
            LaatsteAanpasDatum = date;
            Kosten = new Dictionary<int, KostOfBaat>();
            Baten = new Dictionary<int, KostOfBaat>();
        }

        public Boolean ControleerOfKostMetNummerAlIngevuldIs(int nummer)
        {
            return Kosten.ContainsKey(nummer);
        }

        public KostOfBaat GeefKostMetNummer(int nummer)
        {
            return Kosten[nummer];
        }

        public Boolean ControleerOfBaatMetNummerAlIngevuldIs(int nummer)
        {
            return Baten.ContainsKey(nummer);
        }

        public KostOfBaat GeefBaatMetNummer(int nummer)
        {
            return Baten[nummer];
        }

        public void SlaKostMetNummerOp(int nummer, KostOfBaat kost)
        {
            Kosten[nummer] = kost;
        }

        public void SlaBaatMetNummerOp(int nummer, KostOfBaat baat)
        {
            Baten[nummer] = baat;
        }

        public double GeefSubtotaalKosten()
        {

            return 0;
        }

        public double GeefSubtotaalBaten()
        {

            return 0;
        }

        public double GeefResultaat()
        {
            return GeefSubtotaalBaten() - GeefSubtotaalKosten();
        }

        public void VernieuwDatum()
        {
            LaatsteAanpasDatum = DateTime.Now;
        }
    }
}
