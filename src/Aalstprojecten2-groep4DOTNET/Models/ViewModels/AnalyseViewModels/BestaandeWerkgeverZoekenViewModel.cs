﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class BestaandeWerkgeverZoekenViewModel
    {
        [Display(Name = "Zoeken")]
        public string ZoekString { get; set; }
        public IEnumerable<BestaandeWerkgeverInfoViewModel> Werkgevers { get; set; }
        public bool IsLegeLijst { get; set; }
        public bool HeeftAlGezocht { get; set; }

        public BestaandeWerkgeverZoekenViewModel(IEnumerable<Werkgever> lijst)
        {
            Werkgevers = lijst.Select(w => new BestaandeWerkgeverInfoViewModel(w)).ToList();
            IsLegeLijst = true;
            HeeftAlGezocht = true;
            ZoekString = "";
            foreach (BestaandeWerkgeverInfoViewModel w in Werkgevers)
            {
                IsLegeLijst = false;
                break;
            }
        }

        public BestaandeWerkgeverZoekenViewModel()
        {
            Werkgevers = new List<BestaandeWerkgeverInfoViewModel>();
            IsLegeLijst = true;
            HeeftAlGezocht = false;
        }
    }
}
