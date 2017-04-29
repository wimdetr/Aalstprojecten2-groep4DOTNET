using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class AdminMailRepository:IAdminMailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<AdminMail> _adminMails;

        public AdminMailRepository(ApplicationDbContext context)
        {
            _context = context;
            _adminMails = context.AdminMails;
        }

        public void Add(AdminMail adminMail)
        {
            _adminMails.Add(adminMail);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
