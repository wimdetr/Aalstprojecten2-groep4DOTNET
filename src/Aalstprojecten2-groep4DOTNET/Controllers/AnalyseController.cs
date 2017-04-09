using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Filters;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    [ServiceFilter(typeof(AnalyseFilter))]
    public class AnalyseController : Controller
    {
        private readonly IAnalyseRepository _analyseRepository;
        private readonly IWerkgeverRepository _werkgeverRepository;
        private readonly IJobCoachRepository _jobCoachRepository;

        public AnalyseController(IAnalyseRepository analyseRepository, IWerkgeverRepository werkgeverRepository, IJobCoachRepository jobCoachRepository)
        {
            _analyseRepository = analyseRepository;
            _werkgeverRepository = werkgeverRepository;
            _jobCoachRepository = jobCoachRepository;
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

        public IActionResult AnalyseOverzicht(int id = -1)
        {
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            AnalyseResultaatOverzichtViewModel model;
            if (analyse == null)
            {
                model = new AnalyseResultaatOverzichtViewModel();
            }
            else
            {
                Resultaat resultaat = new Resultaat();
                resultaat.BerekenResultaatVanAnalyse(analyse);
                model = new AnalyseResultaatOverzichtViewModel(analyse);
            }
            return View(model);
        }

        public IActionResult WerkgeverKeuze()
        {
            return View();
        }

        public IActionResult NieuweWerkgever(int id = -1)
        {
            Werkgever werkgever = _werkgeverRepository.GetById(id);
            WerkgeverViewModel model;
            if (werkgever == null)
            {
                model = new WerkgeverViewModel();
            }
            else
            {
                model = new WerkgeverViewModel(werkgever);
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult NieuweWerkgever(WerkgeverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Analyse a = new Analyse(_jobCoachRepository.GetByEmail(User.Identity.Name), DateTime.Now);
                    Werkgever w = new Werkgever(a, model.Naam, model.Postcode, model.Gemeente, model.NaamAfdeling);
                    w.Straat = model.Straat;
                    w.Nummer = model.Nummer;
                    w.Bus = model.Bus;
                    w.AantalWerkuren = model.AantalWerkuren;
                    w.PatronaleBijdrage = model.PatronaleBijdrage;
                    w.LinkNaarLogoPrent = model.LinkNaarLogoPrent;
                    w.ContactPersoonNaam = model.ContactPersoonNaam;
                    w.ContactPersoonVoornaam = model.ContactPersoonVoornaam;
                    w.ContactPersoonEmail = model.ContactPersoonEmail;
                    a.Werkgever = w;
                    _analyseRepository.Add(a);
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseOverzicht));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        public IActionResult BestaandeWerkgever(BestaandeWerkgeverZoekenViewModel m = null)
        {
            BestaandeWerkgeverZoekenViewModel model;
            if (m == null || m.HeeftAlGezocht == false)
            {
                model = new BestaandeWerkgeverZoekenViewModel();
                return View(model);
            }
            model = m;
            return View(model);
        }

        [HttpPost, ActionName("BestaandeWerkgever")]
        public IActionResult BestaandeWerkgeverPost(BestaandeWerkgeverZoekenViewModel model)
        {
            string keuze = Request.Form["zoekKeuze"];
            IEnumerable<Werkgever> werkgevers;
            if (model.ZoekString == null || model.ZoekString.Trim().Equals(""))
            {
                werkgevers = _werkgeverRepository.GetAll(User.Identity.Name);
            }
            else
            {
                switch (keuze)
                {
                    case "1":
                        werkgevers = _werkgeverRepository.GetByNaam(User.Identity.Name, model.ZoekString.Trim());
                        break;
                    case "2":
                        werkgevers = _werkgeverRepository.GetByGemeente(User.Identity.Name, model.ZoekString.Trim());
                        break;
                    case "3":
                        werkgevers = _werkgeverRepository.GetByPostcode(User.Identity.Name, model.ZoekString.Trim());
                        break;
                    case "4":
                        werkgevers = _werkgeverRepository.GetByContactPersoonNaam(User.Identity.Name, model.ZoekString.Trim());
                        break;
                    default:
                        werkgevers = _werkgeverRepository.GetAll(User.Identity.Name);
                        break;
                }
            }
            werkgevers = GeefUniekeWerkgevers(werkgevers);
            model = new BestaandeWerkgeverZoekenViewModel(werkgevers);
            return View(model);
        }

        public IActionResult AnalyseBaat1()
        {
            return View();
        }

        public IActionResult AnalyseKost()
        {
            return View();
        }

        public IEnumerable<Werkgever> GeefUniekeWerkgevers(IEnumerable<Werkgever> lijst)
        {
            ICollection<Werkgever> werkgevers = new List<Werkgever>();

            foreach (Werkgever w in lijst)
            {
                if (!werkgevers.Any(t => t.Equals(w)))
                {
                    werkgevers.Add(w);
                }
            }

            return werkgevers;
        }
    }
}
