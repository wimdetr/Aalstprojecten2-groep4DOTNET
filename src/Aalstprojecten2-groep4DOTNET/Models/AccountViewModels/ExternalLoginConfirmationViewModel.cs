using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "E-mail is verplicht")]
        [Display(Name = "E-mail *")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
