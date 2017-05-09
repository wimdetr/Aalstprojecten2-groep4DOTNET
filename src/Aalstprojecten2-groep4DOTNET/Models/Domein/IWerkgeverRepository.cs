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
        Werkgever GetWithName(string name, string jobcoachEmail);
        Departement GetDepartementByAnalyseId(int id);
        Departement GetDepartementById(int id);
        IEnumerable<Werkgever> GetByNaam(string jobcoachEmail, string naam);
        IEnumerable<Departement> GetAllDepartements(string jobcoachEmail);
        void Add(Werkgever werkgever);
        void Remove(Werkgever werkgever);
        void SaveChanges();
    }
}
