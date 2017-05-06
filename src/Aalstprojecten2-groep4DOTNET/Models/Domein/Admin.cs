using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class Admin
    {
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public bool Superadmin { get; set; }

        public Admin()
        {
            
        }

        public Admin(string email, string naam, string voornaam)
        {
            Email = email;
            Naam = naam;
            Voornaam = voornaam;
            Superadmin = false;
        }
    }
}
