using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Analyse
    {
        #region Properties
        public int Id { get; set; }
        public ICollection<KostOfBaat> Kosten { get; set; }
        
        public ICollection<KostOfBaat> Baten { get; set; }
        public Werkgever Werkgever { get; set; }
        public DateTime LaatsteAanpasDatum { get; set; }
        #endregion

        #region Constructor

        public Analyse(int id, DateTime date)
        {
            Id = id;
            LaatsteAanpasDatum = date;
            Kosten = new List<KostOfBaat>();
            Baten = new List<KostOfBaat>();
        }
        #endregion

        #region methods
        public Boolean ControleerOfKostMetNummerAlIngevuldIs(int nummer)
        {
            return Kosten.Any(k => k.Id == nummer);
        }

        public KostOfBaat GeefKostMetNummer(int nummer)
        {
            return Kosten.FirstOrDefault(k => k.Id == nummer);
        }

        public Boolean ControleerOfBaatMetNummerAlIngevuldIs(int nummer)
        {
            return Baten.Any(b => b.Id == nummer);
        }

        public KostOfBaat GeefBaatMetNummer(int nummer)
        {
            return Baten.FirstOrDefault(b => b.Id == nummer);
        }

        public void SlaKostMetNummerOp(KostOfBaat kost)
        {
            if (ControleerOfKostMetNummerAlIngevuldIs(kost.Id))
            {
                Kosten.Remove(GeefKostMetNummer(kost.Id));
            }
            Kosten.Add(kost);
        }

        public void SlaBaatMetNummerOp(KostOfBaat baat)
        {
            if (ControleerOfBaatMetNummerAlIngevuldIs(baat.Id))
            {
                Baten.Remove(GeefBaatMetNummer(baat.Id));
            }
            Baten.Add(baat);
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
        #endregion
    }
}
