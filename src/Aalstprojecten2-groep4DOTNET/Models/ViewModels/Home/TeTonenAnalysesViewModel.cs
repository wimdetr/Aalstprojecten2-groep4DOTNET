using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class TeTonenAnalysesViewModel
    {
        public IEnumerable<AnalyseOverzichtViewModel> Analyses { get; set; }
        public bool IsLeeg { get; set; }
        public bool ActiveerKnoppen { get; set; }
        public string PdfEncrypted { get; set; } = null;
        public string OntvangerEmail { get; set; } = null;

        public TeTonenAnalysesViewModel(IEnumerable<Analyse> analyses)
        {
            IsLeeg = true;
            Analyses = analyses.Select(a => new AnalyseOverzichtViewModel(a)).ToList();
            int teller = 0;
            foreach (AnalyseOverzichtViewModel a in Analyses)
            {
                IsLeeg = false;
                teller++;
            }
            ActiveerKnoppen = teller > 10;
        }

        public TeTonenAnalysesViewModel()
        {
            
        }
    }
}
