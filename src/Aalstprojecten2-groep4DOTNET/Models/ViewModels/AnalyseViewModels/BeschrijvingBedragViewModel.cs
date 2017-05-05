using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels
{
    public class BeschrijvingBedragViewModel
    {
        [Required(ErrorMessage ="KostId is verplicht, contacteer Bart")]
        public int KostId { get; set; }
        public string Beschrijving { get; set; }
        public double Bedrag { get; set; }
    }
}
