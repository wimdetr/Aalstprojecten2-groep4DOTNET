using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBVak
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public KOBVak(int id)
        {
            Id = id;
        }

        public double GeefDataAlsDouble()
        {
            return Convert.ToDouble(Data);
        }
    }
}
