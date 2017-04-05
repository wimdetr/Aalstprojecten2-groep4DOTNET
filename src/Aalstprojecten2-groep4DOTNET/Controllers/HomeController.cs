using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobCoachRepository _jobCoachRepository;
        private readonly IAnalyseRepository _analyseRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(IJobCoachRepository jobCoachRepository, IAnalyseRepository analyseRepository, UserManager<ApplicationUser> userManager)
        {
            _jobCoachRepository = jobCoachRepository;
            _analyseRepository = analyseRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
            jc.Analyses = _analyseRepository.GetAll(jc.Email);
            Resultaat r = new Resultaat();
            foreach (Analyse a in jc.Analyses)
            {
                r.BerekenResultaatVanAnalyse(a);
            }
            return View(new TeTonenAnalysesViewModel(jc.Analyses));
        }
    }
}
