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
        public bool Gelezen { get; set; }
        public string Onderwerp { get; set; }
        public string Inhoud { get; set; }
        public string DatumTijd { get; set; }

        public MailViewModel(InterneMailJobcoach m)
        {
            MailId = m.InterneMailId;
            Afzender = m.InterneMail.Afzender;
            Gelezen = m.IsGelezen;
            Onderwerp = m.InterneMail.Onderwerp;
            Inhoud = m.InterneMail.Inhoud;
            DateTime dt = m.InterneMail.VerzendDatum;
            DatumTijd = dt.Day + "/" + dt.Month + "/" + dt.Year + " - " + dt.Hour + ":" + dt.Minute;
        }
    }
}
