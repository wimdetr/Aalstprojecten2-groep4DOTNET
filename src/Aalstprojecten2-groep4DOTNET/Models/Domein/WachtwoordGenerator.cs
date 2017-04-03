using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class WachtwoordGenerator
    {
        public static string GeefRandomWachtwoord()
        {
            Random rd = new Random();
            int lengte = rd.Next(6, 11);

            StringBuilder wachtwoord = new StringBuilder();

            for (int i = 0; i < lengte; i++)
            {
                switch (rd.Next(1, 4))
                {
                    case 1:
                        wachtwoord.Append((char) rd.Next(48, 58));
                        break;
                    case 2:
                        wachtwoord.Append((char)rd.Next(65, 91));
                        break;
                    case 3:
                        wachtwoord.Append((char)rd.Next(97, 128));
                        break;
                }
            }

            return wachtwoord.ToString();
        }
    }
}
