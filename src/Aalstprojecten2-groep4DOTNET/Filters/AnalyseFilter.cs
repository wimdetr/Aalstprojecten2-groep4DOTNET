using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Aalstprojecten2_groep4DOTNET.Filters
{
    public class AnalyseFilter: ActionFilterAttribute
    {
        private readonly IWerkgeverRepository _werkgeverRepository;
        private Analyse _analyse;

        public AnalyseFilter(IWerkgeverRepository werkgeverRepository)
        {
            _werkgeverRepository = werkgeverRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _analyse = ReadAnalyseFromSession(context.HttpContext);
            context.ActionArguments["analyse"] = _analyse;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteAnalyseToSession(_analyse, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private Analyse ReadAnalyseFromSession(HttpContext context)
        {
            Analyse analyse = context.Session.GetString("analyse") == null
                ? new Analyse()
                : JsonConvert.DeserializeObject<Analyse>(context.Session.GetString("analyse"));

            analyse.Werkgever = _werkgeverRepository.GetByAnalyseId(analyse.AnalyseId);
            return analyse;
        }

        private void WriteAnalyseToSession(Analyse analyse, HttpContext context)
        {
            context.Session.SetString("analyse", JsonConvert.SerializeObject(analyse));
        }

        public static void PlaatsAnalyseInSession(Analyse analyse, HttpContext context)
        {
            context.Session.SetString("analyse", JsonConvert.SerializeObject(analyse));
        }

        public static void ZetSessieLeeg(HttpContext context)
        {
            context.Session.Clear();
        }
    }
}
