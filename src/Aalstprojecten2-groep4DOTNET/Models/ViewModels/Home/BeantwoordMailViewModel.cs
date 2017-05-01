﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;

namespace Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home
{
    public class BeantwoordMailViewModel
    {
        [Required]
        public string AdminMail { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "{0} mag maximaal 100 karakters bevatten!")]
        public string Onderwerp { get; set; }
        [Required]
        public string Inhoud { get; set; }

        public BeantwoordMailViewModel()
        {
            
        }

        public BeantwoordMailViewModel(InterneMailJobcoach m)
        {
            AdminMail = m.InterneMail.Afzender;
            Onderwerp = "RE: " + m.InterneMail.Onderwerp;
            Inhoud = "\n\n\n" + m.InterneMail.Inhoud;
        }
    }
}