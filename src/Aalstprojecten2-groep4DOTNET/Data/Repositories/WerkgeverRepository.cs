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

        public WerkgeverRepository(ApplicationDbContext context)
        {
            _context = context;
            _werkgevers = context.Werkgevers;
        }
        public IEnumerable<Werkgever> GetAll(string jobcoachEmail)
        {
            return _werkgevers.Where(w => w.JobCoachEmail.Equals(jobcoachEmail)).AsNoTracking().ToList();
        }

        public Werkgever GetById(int id)
        {
            return _werkgevers.SingleOrDefault(w => w.WerkgeverId == id);
        }

        public IEnumerable<Werkgever> GetByNaam(string jobcoachEmail, string naam)
        {
            return
                _werkgevers
                    .Where(w => w.JobCoachEmail.Equals(jobcoachEmail) && w.Naam.Contains(naam))
                    .AsNoTracking()
                    .ToList();
        }

        public IEnumerable<Werkgever> GetByGemeente(string jobcoachEmail, string gemeente)
        {
            return
                _werkgevers
                    .Where(w => w.JobCoachEmail.Equals(jobcoachEmail) && w.Gemeente.Contains(gemeente))
                    .AsNoTracking()
                    .ToList();
        }

        public IEnumerable<Werkgever> GetByPostcode(string jobcoachEmail, string postcode)
        {
            return
                _werkgevers
                    .Where(w => w.JobCoachEmail.Equals(jobcoachEmail) && w.Postcode.ToString().Contains(postcode))
                    .AsNoTracking()
                    .ToList();
        }

        public IEnumerable<Werkgever> GetByContactPersoonNaam(string jobcoachEmail, string naam)
        {
            return
                _werkgevers
                    .Where(w => w.JobCoachEmail.Equals(jobcoachEmail) && ((w.ContactPersoonVoornaam + " " + w.ContactPersoonNaam).Contains(naam) || (w.ContactPersoonNaam + " " + w.ContactPersoonVoornaam).Contains(naam)))
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
