using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class JobCoachRepository: IJobCoachRepository
    {
        private ApplicationDbContext _context;
        private readonly DbSet<JobCoach> _jobCoaches;

        public JobCoachRepository(ApplicationDbContext context)
        {
            _context = context;
            _jobCoaches = context.JobCoaches;
        }

        public JobCoach GetByEmail(string email)
        {
            return _jobCoaches.Include(jc => jc.Analyses).SingleOrDefault(jc => jc.Email.Equals(email));
        }

        public void Add(JobCoach jobCoach)
        {
            _jobCoaches.Add(jobCoach);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
