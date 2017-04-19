using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class BedragPercentViewModel
    {
        [Required]
        public int KostId { get; set; }
        public double Bedrag { get; set; }
        public int Percent { get; set; }
    }
}
