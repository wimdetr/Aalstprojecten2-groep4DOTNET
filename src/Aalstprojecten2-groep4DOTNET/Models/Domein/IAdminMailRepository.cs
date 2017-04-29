using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public interface IAdminMailRepository
    {
        void Add(AdminMail adminMail);
        void SaveChanges();
    }
}
