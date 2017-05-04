using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class MailViewModel
    {
        public int MailId { get; set; }
        public string Afzender { get; set; }
        public string AfzenderKort { get; set; }
        public string AfzenderVolledigeNaam { get; set; }
        public bool Gelezen { get; set; }
        public string Onderwerp { get; set; }
        public string OnderwerpKort { get; set; }
        public string Inhoud { get; set; }
        public string InhoudKort { get; set; }
        public string DatumTijdVolledig { get; set; }
        public string DatumTijdKort { get; set; }

        public MailViewModel(InterneMailJobcoach m)
        {
            MailId = m.InterneMailId;
            Afzender = m.InterneMail.Afzender;
            AfzenderKort = Afzender.Length > 20 ? Afzender.Substring(0, 20) + "..." : Afzender;
            AfzenderVolledigeNaam = "Van " + m.Jobcoach.Voornaam + " " + m.Jobcoach.Naam;
            Gelezen = m.IsGelezen;
            Onderwerp = m.InterneMail.Onderwerp;
            OnderwerpKort = Onderwerp.Length > 20 ? Onderwerp.Substring(0, 20) + "..." : Onderwerp;
            Inhoud = m.InterneMail.Inhoud;
            InhoudKort = Inhoud.Length > 20 ? Inhoud.Substring(0, 20) + "..." : Inhoud;
            DateTime dt = m.InterneMail.VerzendDatum;
            if (dt > DateTime.Now.AddDays(-1))
            {
                DatumTijdKort = dt.Hour + ":" + dt.Minute;
            }
            else if (dt > DateTime.Now.AddDays(-7))
            {
                DatumTijdKort = "";
                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        DatumTijdKort = "ma";
                        break;
                    case DayOfWeek.Tuesday:
                        DatumTijdKort = "di";
                        break;
                    case DayOfWeek.Wednesday:
                        DatumTijdKort = "wo";
                        break;
                    case DayOfWeek.Thursday:
                        DatumTijdKort = "do";
                        break;
                    case DayOfWeek.Friday:
                        DatumTijdKort = "vr";
                        break;
                    case DayOfWeek.Saturday:
                        DatumTijdKort = "za";
                        break;
                    case DayOfWeek.Sunday:
                        DatumTijdKort = "zo";
                        break;
                }
                DatumTijdKort += " " + dt.Hour + ":" + dt.Minute;
            }
            else
            {
                DatumTijdKort = dt.Day + "/" + dt.Month + "/" + dt.Year;
            }
            DatumTijdVolledig = dt.Day + "/" + dt.Month + "/" + dt.Year + " - " + dt.Hour + ":" + dt.Minute;
        }
    }
}
