using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public class KOBVak
    {
        #region Properties

        public int AnalyseId { get; set; }
        public string JobCoachEmail { get; set; }
        public int KostOfBaatId { get; set; }
        public KOBEnum KostOfBaatEnum { get; set; }
        public int KOBRijId { get; set; }
        public int KOBVakId { get; set; }
        public string Data { get; set; }
        #endregion

        #region Contructor
        public KOBVak(KOBRij kobRij, int id): this(kobRij, id, "")
        {
        }

        public KOBVak(KOBRij kobRij, int id, string data)
        {
            KOBRijId = kobRij.KOBRijId;
            KostOfBaatEnum = kobRij.KostOfBaatEnum;
            KostOfBaatId = kobRij.KostOfBaatId;
            JobCoachEmail = kobRij.JobCoachEmail;
            AnalyseId = kobRij.AnalyseId;
            KOBVakId = id;
            Data = data;
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
