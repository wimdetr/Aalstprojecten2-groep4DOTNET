using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class AnalyseController : Controller
    {
        private readonly IAnalyseRepository _analyseRepository;

        public AnalyseController(IAnalyseRepository analyseRepository)
        {
            _analyseRepository = analyseRepository;
        }
        // GET: /<controller>/
        public IActionResult AnalyseBekijken()
        {
            IEnumerable<Analyse> analyses = _analyseRepository.GetAllWelGearchiveerd(User.Identity.Name);
            Resultaat r = new Resultaat();
            foreach (Analyse a in analyses)
            {
                r.BerekenResultaatVanAnalyse(a);
            }
            return View(new TeTonenAnalysesViewModel(analyses));
        }

        public IActionResult Archiveer(int id)
        {
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
            {
                return NotFound();
            }
            ViewData["analyse"] = analyse.Werkgever.Naam + " - " + analyse.Werkgever.NaamAfdeling;
            return View();
        }

        [HttpPost, ActionName("Archiveer")]
        public IActionResult BevestigArchiveer(int id)
        {
            try
            {
                Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
                analyse.IsGearchiveerd = true;
                _analyseRepository.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeArchiveer(int id)
        {
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
            {
                return NotFound();
            }
            ViewData["analyse"] = analyse.Werkgever.Naam + " - " + analyse.Werkgever.NaamAfdeling;
            return View();
        }

        [HttpPost, ActionName("DeArchiveer")]
        public IActionResult BevestigDeArchiveer(int id)
        {
            try
            {
                Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
                analyse.IsGearchiveerd = false;
                _analyseRepository.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction(nameof(AnalyseBekijken));
        }

        public IActionResult Delete(int id)
        {
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
                return NotFound();
            ViewData["analyse"] = analyse.Werkgever.Naam + " - " + analyse.Werkgever.NaamAfdeling;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
                _analyseRepository.Delete(analyse);
                _analyseRepository.SaveChanges();
            }
            catch
            {
                
            }
            return RedirectToAction(nameof(AnalyseBekijken));
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
