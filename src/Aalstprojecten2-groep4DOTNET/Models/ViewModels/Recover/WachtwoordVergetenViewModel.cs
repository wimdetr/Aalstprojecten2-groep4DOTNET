using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Recover
{
    public class WachtwoordVergetenViewModel
    {
        [Required(ErrorMessage ="E-mail is verplicht")]
        [Display(Name ="E-mail *")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
