using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class InterneMail
    {
        #region Properties

        public int InterneMailId { get; set; }
        public string Afzender { get; set; }
        public string Onderwerp { get; set; }
        public string Inhoud { get; set; }
        public DateTime VerzendDatum { get; set; }

        #endregion

        #region Constructors

        public InterneMail()
        {
        }

        public InterneMail(string afzender, string onderwerp, string inhoud, DateTime verzendDatum)
        {
            Afzender = afzender;
            Onderwerp = onderwerp;
            Inhoud = inhoud;
            VerzendDatum = verzendDatum;
        }

        #endregion
    }
}
