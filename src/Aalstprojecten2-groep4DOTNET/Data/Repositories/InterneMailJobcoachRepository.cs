﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore;

namespace Aalstprojecten2_groep4DOTNET.Data.Repositories
{
    public class InterneMailJobcoachRepository: IInterneMailJobcoachRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<InterneMailJobcoach> _interneMailJobcoaches;

        public InterneMailJobcoachRepository(ApplicationDbContext context)
        {
            _context = context;
            _interneMailJobcoaches = context.InterneMailJobcoaches;
        }
        public IEnumerable<InterneMailJobcoach> GetAll(string jobcoachEmail)
        {
            return _interneMailJobcoaches.Include(i => i.InterneMail).Where(i => i.JobcoachEmail.Equals(jobcoachEmail)).AsNoTracking().ToList();
        }

        public InterneMailJobcoach GetById(int id)
        {
            return _interneMailJobcoaches.Include(i => i.InterneMail).SingleOrDefault(i => i.InterneMailId == id);
        }

        public void Delete(InterneMailJobcoach interneMailJobcoach)
        {
            _interneMailJobcoaches.Remove(interneMailJobcoach);
        }

        public void Add(InterneMailJobcoach interneMailJobcoach)
        {
            _interneMailJobcoaches.Add(interneMailJobcoach);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}