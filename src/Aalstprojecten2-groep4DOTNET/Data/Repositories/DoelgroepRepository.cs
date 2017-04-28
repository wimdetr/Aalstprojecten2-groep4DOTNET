using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class DoelgroepRepository:IDoelgroepRepository
    {
        private readonly DbSet<Doelgroep> _doelgroepen;

        public DoelgroepRepository(ApplicationDbContext context)
        {
            _doelgroepen = context.Doelgroepen;
        }

        public IEnumerable<Doelgroep> GetAll()
        {
            return _doelgroepen.AsNoTracking().ToList();
        }

        public IEnumerable<Doelgroep> GetAllSelecteerbaar()
        {
            return _doelgroepen.Where(d => d.IsVerwijdert == false).AsNoTracking().ToList();
        }
    }
}
