using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class JobCoach : Persoon
    {
        public ICollection<Analyse> Analyses { get; set; }
        public string NaamBedrijf { get; set; }
        public string StraatBedrijf { get; set; }
        public string NummerBedrijf { get; set; }
        public int PostcodeBedrijf { get; set; }
        public string GemeenteBedrijf { get; set; }

        public JobCoach(string naam, string voornaam, string email, string naamBedrijf, string straatBedrijf, string nummerBedrijf, int postcodeBedrijf, string gemeenteBedrijf): base(naam, voornaam, email)
        {
            Analyses = new List<Analyse>();
            NaamBedrijf = naamBedrijf;
            StraatBedrijf = straatBedrijf;
            NummerBedrijf = nummerBedrijf;
            PostcodeBedrijf = postcodeBedrijf;
            GemeenteBedrijf = gemeenteBedrijf;
        }

        
    }
}
