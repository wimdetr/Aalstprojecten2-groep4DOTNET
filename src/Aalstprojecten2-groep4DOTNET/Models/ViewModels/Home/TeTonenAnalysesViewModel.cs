﻿using System;
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

        public TeTonenAnalysesViewModel(IEnumerable<Analyse> analyses)
        {
            IsLeeg = true;
            Analyses = analyses.Select(a => new AnalyseOverzichtViewModel(a)).ToList();
            foreach (AnalyseOverzichtViewModel a in Analyses)
            {
                IsLeeg = false;
                break;
            }
        }
    }
}