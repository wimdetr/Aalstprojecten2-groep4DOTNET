using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public interface IWerkgeverRepository
    {
        IEnumerable<Werkgever> GetAll(string jobcoachEmail);
        Werkgever GetById(int id);
        Werkgever GetByAnalyseId(int id);
        IEnumerable<Werkgever> GetByNaam(string jobcoachEmail, string naam);
        IEnumerable<Werkgever> GetByGemeente(string jobcoachEmail, string gemeente);
        IEnumerable<Werkgever> GetByPostcode(string jobcoachEmail, string postcode);
        IEnumerable<Werkgever> GetByContactPersoonNaam(string jobcoachEmail, string naam);
        void Add(Werkgever werkgever);
        void Remove(Werkgever werkgever);
        void SaveChanges();
    }
}
