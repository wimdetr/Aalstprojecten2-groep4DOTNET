using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBVak
    {
        #region Properties
        public int Id { get; set; }
        public string Data { get; set; }
        #endregion

        #region Contructor
        public KOBVak(int id)
        {
            Id = id;
        }
        #endregion

        #region methods
        public double GeefDataAlsDouble()
        {
            return Convert.ToDouble(Data);
        }
        #endregion
    }
}
