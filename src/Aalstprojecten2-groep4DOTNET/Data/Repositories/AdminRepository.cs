using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly DbSet<Admin> _admins;

        public AdminRepository(ApplicationDbContext context)
        {
            _admins = context.Admins;
        }
        public Admin GetByEmail(string email)
        {
            return _admins.SingleOrDefault(a => a.Email.Equals(email));
        }
    }
}
