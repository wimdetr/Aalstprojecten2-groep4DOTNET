using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aalstprojecten2_groep4DOTNET.Models.Domein
{
    public enum AnalyseDoelgroep
    {
        //display voor string weergave
        WnMinderDan25JaarLaaggeschoold = 1550,
        WnMinderDan25JaarMiddengeschoold = 1000,
        WnMeerOfGelijkAan55OfMinderDan60Jaar = 1150,
        WnMeerOfEvenveelAls60Jaar = 1500,
        Ander = 0
    }
}
