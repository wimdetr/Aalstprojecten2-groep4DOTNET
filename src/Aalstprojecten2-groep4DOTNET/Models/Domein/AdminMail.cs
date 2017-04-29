using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class AdminMail
    {
        public int AdminMailId { get; set; }
        public JobCoach Afzender { get; set; }
        public string AfzenderMail { get; set; }
        public string Ontvanger { get; set; }
        public string Onderwerp { get; set; }
        public string Inhoud { get; set; }
        public DateTime VerzendDatum { get; set; }
        public bool IsGelezen { get; set; }

        protected AdminMail()
        {
            
        }

        public AdminMail(JobCoach jc, string ontvanger, string onderwerp, string inhoud, DateTime verzendDatum)
        {
            Afzender = jc;
            AfzenderMail = jc.Email;
            Ontvanger = ontvanger;
            Onderwerp = onderwerp;
            Inhoud = inhoud;
            VerzendDatum = verzendDatum;
            IsGelezen = false;
        }
    }
}
