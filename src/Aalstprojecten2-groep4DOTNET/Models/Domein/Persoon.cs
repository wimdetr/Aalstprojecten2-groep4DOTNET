using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public abstract class Persoon
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }

        protected Persoon(string naam, string voornaam, string email)
        {
            Naam = naam;
            Voornaam = voornaam;
            Email = email;
        }
    }
}
