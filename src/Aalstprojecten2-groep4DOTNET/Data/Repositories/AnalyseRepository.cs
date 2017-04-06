using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class AnalyseRepository: IAnalyseRepository
    {
        private ApplicationDbContext _context;
        private readonly DbSet<Analyse> _analyses;

        public AnalyseRepository(ApplicationDbContext context)
        {
            _context = context;
            _analyses = context.Analyses;
        }

        public IEnumerable<Analyse> GetAllNietGearchiveerd(string jobcoachEmail)
        {
            return _analyses.Include(a => a.Werkgever).Include(a => a.KostenEnBaten).ThenInclude(kob => kob.Rijen).ThenInclude(rij => rij.Vakken).Where(a => a.JobCoachEmail.Equals(jobcoachEmail) && a.IsGearchiveerd == false).AsNoTracking().ToList();
        }

        public IEnumerable<Analyse> GetAllWelGearchiveerd(string jobcoachEmail)
        {
            return _analyses.Include(a => a.Werkgever).Include(a => a.KostenEnBaten).ThenInclude(kob => kob.Rijen).ThenInclude(rij => rij.Vakken).Where(a => a.JobCoachEmail.Equals(jobcoachEmail) && a.IsGearchiveerd == true).AsNoTracking().ToList();
        }

        public Analyse GetById(string jobcoachEmail, int id)
        {
            return
                _analyses.Include(a => a.Werkgever)
                    .Include(a => a.KostenEnBaten)
                    .ThenInclude(kob => kob.Rijen)
                    .ThenInclude(rij => rij.Vakken)
                    .SingleOrDefault(a => a.JobCoachEmail.Equals(jobcoachEmail) && a.AnalyseId == id);
        }

        public void Add(Analyse analyse)
        {
            _analyses.Add(analyse);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
