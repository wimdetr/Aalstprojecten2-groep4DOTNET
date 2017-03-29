using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein.HulpKlassenAnalyseDoelgroep;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public enum AnalyseDoelgroep
    {
        //display voor string weergave
        [StringWaarde("wn minder dan 25 jaar laaggeschoold")]
        WnMinderDan25JaarLaaggeschoold = 1550,
        [StringWaarde("wn minder dan 25 jaar middengeschoold")]
        WnMinderDan25JaarMiddengeschoold = 1000,
        [StringWaarde("wn meer of gelijk aan 55 of minder dan 60 jaar")]
        WnMeerOfGelijkAan55OfMinderDan60Jaar = 1150,
        [StringWaarde("wn meer of evenveel als 60 jaar")]
        WnMeerOfEvenveelAls60Jaar = 1500,
        [StringWaarde("ander")]
        Ander = 0
    }
}
