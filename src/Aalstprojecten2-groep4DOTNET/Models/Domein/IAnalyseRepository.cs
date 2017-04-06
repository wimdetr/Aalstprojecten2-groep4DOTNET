using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public interface IAnalyseRepository
    {
        IEnumerable<Analyse> GetAllNietGearchiveerd(string jobcoachEmail);
        IEnumerable<Analyse> GetAllWelGearchiveerd(string jobcoachEmail);
        Analyse GetById(string jobcoachEmail, int id);
        void Add(Analyse analyse);
        void SaveChanges();
    }
}
