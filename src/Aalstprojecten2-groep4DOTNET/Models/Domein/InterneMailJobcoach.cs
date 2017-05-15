using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class InterneMailJobcoach
    {
        #region Properties
        public int Id { get; set; }
        public InterneMail InterneMail { get; set; }
        public int InterneMailId { get; set; }
        public JobCoach Jobcoach { get; set; }
        public string JobcoachEmail { get; set; }
        public bool IsGelezen { get; set; }

        #endregion

        #region Constructors

        public InterneMailJobcoach()
        {
            
        }

        public InterneMailJobcoach(JobCoach jc, InterneMail mail)
        {
            Jobcoach = jc;
            JobcoachEmail = jc.Email;

            InterneMail = mail;
            InterneMailId = mail.InterneMailId;
        }

        #endregion
    }
}
