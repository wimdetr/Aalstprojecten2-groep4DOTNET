using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class WerkgeverRepository:IWerkgeverRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Werkgever> _werkgevers;
        private readonly DbSet<Departement> _departementen;

        public WerkgeverRepository(ApplicationDbContext context)
        {
            _context = context;
            _werkgevers = context.Werkgevers;
            _departementen = context.Departementen;
        }
        public IEnumerable<Werkgever> GetAll(string jobcoachEmail)
        {
            return _werkgevers.Where(w => w.JobCoachEmail.Equals(jobcoachEmail)).AsNoTracking().ToList();
        }

        public IEnumerable<Departement> GetAllDepartements(string jobcoachEmail)
        {
            return
                _departementen.Include(d => d.Werkgever)
                    .Where(d => d.Werkgever.JobCoachEmail.Equals(jobcoachEmail))
                    .AsNoTracking()
                    .ToList();
        }
        public Werkgever GetById(int id)
        {
            return _werkgevers.SingleOrDefault(w => w.WerkgeverId == id);
        }

        public Werkgever GetWithName(string name, string jobcoachEmail)
        {
            return _werkgevers.SingleOrDefault(w => w.Naam.Equals(name) && w.JobCoachEmail.Equals(jobcoachEmail));
        }

        public Departement GetDepartementById(int id)
        {
            return _departementen.Include(d => d.Werkgever).SingleOrDefault(d => d.AnalyseId == id);
        }

        public Departement GetDepartementByAnalyseId(int id)
        {
            return _departementen.Include(d => d.Werkgever).SingleOrDefault(w => w.AnalyseId == id);
        }

        public IEnumerable<Werkgever> GetByNaam(string jobcoachEmail, string naam)
        {
            return
                _werkgevers
                    .Where(w => w.JobCoachEmail.Equals(jobcoachEmail) && w.Naam.ToLower().Contains(naam.ToLower()))
                    .AsNoTracking()
                    .ToList();
        }

        public void Add(Werkgever werkgever)
        {
            _werkgevers.Add(werkgever);
        }

        public void Remove(Werkgever werkgever)
        {
            _werkgevers.Remove(werkgever);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
