using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
