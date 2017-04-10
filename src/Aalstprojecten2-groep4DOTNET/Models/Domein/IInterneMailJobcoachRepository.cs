using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public interface IInterneMailJobcoachRepository
    {
        IEnumerable<InterneMailJobcoach> GetAll(string jobcoachEmail);
        InterneMailJobcoach GetById(string jobcoachEmail, int id);
        int GetAantalOngelezen(string jobcoachEmail);
        void Delete(InterneMailJobcoach interneMailJobcoach);
        void Add(InterneMailJobcoach interneMailJobcoach);
        void SaveChanges();
    }
}
