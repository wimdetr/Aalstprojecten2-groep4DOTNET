﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class ContacteerAdminViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "{0} mag maximaal 100 karakters bevatten!")]
        public string Onderwerp { get; set; }
        [Required]
        public string Inhoud { get; set; }
    }
}