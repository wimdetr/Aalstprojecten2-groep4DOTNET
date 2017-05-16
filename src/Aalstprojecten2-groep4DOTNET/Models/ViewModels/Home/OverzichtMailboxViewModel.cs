using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class OverzichtMailboxViewModel
    {
        private int _geopendeMailId;
        public IEnumerable<MailViewModel> Mails { get; set; }
        public bool IsLegeLijst { get; set; }
        public MailViewModel GeopendeMail { get; set; } = null;
        public string Ontvanger { get; set; }
        public string Onderwerp { get; set; }
        public string Inhoud { get; set; }
        public bool WilAnimaties { get; set; }

        public int GeopendeMailId
        {
            get { return GeopendeMail == null ? -1 : _geopendeMailId; } 
            set { _geopendeMailId = value; }
        }

        public OverzichtMailboxViewModel(IEnumerable<InterneMailJobcoach> mails)
        {
            IsLegeLijst = true;
            Mails = mails.Select(m => new MailViewModel(m)).ToList();
            foreach (MailViewModel m in Mails)
            {
                IsLegeLijst = false;
                break;
            }
            
        }

        public OverzichtMailboxViewModel()
        {
            
        }
    }
}
