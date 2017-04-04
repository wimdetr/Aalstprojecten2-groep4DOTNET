using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.NogViewModels
{
    public class VeranderWachtwoordViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht in te vullen")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen")]
        [DataType(DataType.Password)]
        [Compare("Wachtwoord", ErrorMessage = "De 2 wachtwoorden komen niet overeen.")]
        public string BevestigWachtwoord { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
