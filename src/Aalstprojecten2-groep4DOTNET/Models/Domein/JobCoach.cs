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
        public int NummerBedrijf { get; set; }
        public string BusBedrijf { get; set; }
        public int PostcodeBedrijf { get; set; }
        public string GemeenteBedrijf { get; set; }

        public JobCoach(string naam, string voornaam, string email, string naamBedrijf, string straatBedrijf, int nummerBedrijf, int postcodeBedrijf, string gemeenteBedrijf): base(naam, voornaam, email)
        {
            Analyses = new List<Analyse>();
            NaamBedrijf = naamBedrijf;
            StraatBedrijf = straatBedrijf;
            NummerBedrijf = nummerBedrijf;
            PostcodeBedrijf = postcodeBedrijf;
            GemeenteBedrijf = gemeenteBedrijf;
        }

        public JobCoach(string naam, string voornaam, string email, string naamBedrijf, string straatBedrijf,
            int nummerBedrijf, string busBedrijf, int postcodeBedrijf, string gemeenteBedrijf)
            : this(naam, voornaam, email, naamBedrijf, straatBedrijf, nummerBedrijf, postcodeBedrijf, gemeenteBedrijf)
        {
            BusBedrijf = busBedrijf;
        }

        public bool ControleerOfAnalyseAanwezigIs(int id)
        {
            return Analyses.Any(a => a.AnalyseId == id);
        }

        public void VoegAnalyseToe(Analyse a)
        {
            Analyses.Add(a);
        }

        public Analyse GeefAnalyseMetId(int id)
        {
            return Analyses.FirstOrDefault(a => a.AnalyseId == id);
        }
    }
}
