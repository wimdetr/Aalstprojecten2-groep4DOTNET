using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class AnalyseController : Controller
    {
        // GET: /<controller>/
        public IActionResult AnalyseBekijken()
        {
            return View();
        }

        public IActionResult AnalyseOverzicht()
        {
            return View();
        }

        public IActionResult AnalyseBaat1()
        {
            return View();
        }

        public IActionResult AnalyseKost()
        {
            return View();
        }
    }
}
