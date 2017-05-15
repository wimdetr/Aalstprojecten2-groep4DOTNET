using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Filters;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    [Authorize]
    public class AnalyseController : Controller
    {
        private readonly IAnalyseRepository _analyseRepository;
        private readonly IWerkgeverRepository _werkgeverRepository;
        private readonly IJobCoachRepository _jobCoachRepository;
        private readonly IDoelgroepRepository _doelgroepRepository;
        private readonly IHostingEnvironment _environment;

        public AnalyseController(IAnalyseRepository analyseRepository, IWerkgeverRepository werkgeverRepository, IJobCoachRepository jobCoachRepository, IDoelgroepRepository doelgroepRepository, IHostingEnvironment environment)
        {
            _analyseRepository = analyseRepository;
            _werkgeverRepository = werkgeverRepository;
            _jobCoachRepository = jobCoachRepository;
            _doelgroepRepository = doelgroepRepository;
            _environment = environment;
        }
        // GET: /<controller>/
        public IActionResult AnalyseBekijken()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            IEnumerable<Analyse> analyses = _analyseRepository.GetAllWelGearchiveerd(User.Identity.Name);
            Resultaat r = new Resultaat(_doelgroepRepository.GetAll());
            foreach (Analyse a in analyses)
            {
                r.BerekenResultaatVanAnalyse(a);
            }
            return View(new TeTonenAnalysesViewModel(analyses));
        }

        public IActionResult Archiveer(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["analyse"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View();
        }

        [HttpPost, ActionName("Archiveer")]
        public IActionResult BevestigArchiveer(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            try
            {
                Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
                analyse.IsGearchiveerd = true;
                _analyseRepository.SaveChanges();
                TempData["message"] = "De analyse voor " + analyse.Departement.Werkgever.Naam + " - " +
                                      analyse.Departement.Naam + " is succesvol gearchiveerd.";
            }
            catch
            {
                TempData["error"] = "Iets is misgelopen, de analyse is niet gearchiveerd.";
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
                return RedirectToAction("Index", "Home");
            ViewData["analyse"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            try
            {
                Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
                TempData["message"] = "De analyse voor " + analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam + " is succesvol verwijderd.";
                _analyseRepository.Delete(analyse);
                _analyseRepository.SaveChanges();
            }
            catch
            {
                TempData["error"] = "Iets is misgelopen, de analyse is niet verwijderd.";
            }
            return RedirectToAction(nameof(AnalyseBekijken));
        }

        public IActionResult PasAnalyseAan(int id = -1)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, id);
            AnalyseFilter.PlaatsAnalyseInSession(a, HttpContext);
            return RedirectToAction(nameof(AnalyseOverzicht));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseOverzicht(Analyse analyse)
        {
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            AnalyseResultaatOverzichtViewModel model;
            if (analyse == null)
            {
                model = new AnalyseResultaatOverzichtViewModel();
            }
            else
            {
                Resultaat resultaat = new Resultaat(_doelgroepRepository.GetAll());
                resultaat.BerekenResultaatVanAnalyse(analyse);
                model = new AnalyseResultaatOverzichtViewModel(analyse);
            }
            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        public IActionResult BekijkGearchiveerdeAnalyse(int id = -1)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, id);
            AnalyseFilter.PlaatsAnalyseInSession(a, HttpContext);
            return RedirectToAction(nameof(AnalyseOverzichtBekijken));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseOverzichtBekijken(Analyse analyse)
        {
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            AnalyseResultaatOverzichtViewModel model;
            if (analyse == null)
            {
                model = new AnalyseResultaatOverzichtViewModel();
            }
            else
            {
                Resultaat resultaat = new Resultaat(_doelgroepRepository.GetAll());
                resultaat.BerekenResultaatVanAnalyse(analyse);
                model = new AnalyseResultaatOverzichtViewModel(analyse);
            }
            model.IsVoltooid = true;
            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(nameof(AnalyseOverzicht), model);
        }

        public IActionResult WerkgeverKeuze()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return View();
        }

        public IActionResult NieuweWerkgever(int id = -1)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Werkgever werkgever = _werkgeverRepository.GetById(id);
            var model = werkgever == null ? new WerkgeverViewModel() : new WerkgeverViewModel(werkgever);
            model.Titel = "Nieuwe Analyse";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NieuweWerkgever(IFormFile file, WerkgeverViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            model.Titel = "Nieuwe Analyse";
            string origineelWerkgeverNaam;
            if (model.DepartementId != null)
            {
                origineelWerkgeverNaam =
                    _werkgeverRepository.GetDepartementById(model.DepartementId.Value).Werkgever.Naam;
            }
            else
            {
                origineelWerkgeverNaam = model.Naam;
            }
            if (ModelState.IsValid &&
                _analyseRepository.GetAllNietGearchiveerd(User.Identity.Name).Any(a => ControlleerOfDepartementHetzelfdeIs(a.Departement, model, origineelWerkgeverNaam)))
            {
                ModelState.AddModelError("NaamAfdeling", "Er is al een actieve analyse voor deze dienst");
            }
            if (ModelState.IsValid)
            {
                try
                {


                    Analyse a = new Analyse(_jobCoachRepository.GetByEmail(User.Identity.Name), DateTime.Now);

                    Werkgever w = _werkgeverRepository.GetWithName(model.Naam, User.Identity.Name);
                    if (w == null)
                    {
                        w = new Werkgever(User.Identity.Name, model.Naam)
                        {
                            PatronaleBijdrage = model.PatronaleBijdrage,
                        };
                    }

                    Departement d = new Departement(a, w, model.NaamAfdeling, model.Straat, model.Nummer, model.Bus, model.Postcode, model.Gemeente)
                    {
                        AantalWerkuren = model.AantalWerkuren,
                        ContactPersoonNaam = model.ContactPersoonNaam,
                        ContactPersoonVoornaam = model.ContactPersoonVoornaam,
                        ContactPersoonEmail = model.ContactPersoonEmail
                    };

                    a.Departement = d;
                    _analyseRepository.Add(a);
                    _analyseRepository.SaveChanges();

                    
                    if (file != null && file.Length > 0)
                    {
                        var upload = Path.Combine(_environment.WebRootPath, "images\\uploads");
                        string[] gesplitst = file.FileName.Split('.');
                        string linkNaarLogoPrent = User.Identity.Name + "_" + a.Departement.Werkgever.WerkgeverId + "." +
                                                   gesplitst[gesplitst.Length - 1];
                        using (
                            var fileStream =
                                new FileStream(
                                    Path.Combine(upload, linkNaarLogoPrent),
                                    FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        _werkgeverRepository.GetById(a.Departement.Werkgever.WerkgeverId).LinkNaarLogoPrent =
                        "/images/uploads/" + linkNaarLogoPrent;
                        _werkgeverRepository.SaveChanges();
                    }
                    
                    AnalyseFilter.PlaatsAnalyseInSession(a, HttpContext);
                    return RedirectToAction(nameof(AnalyseKost));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult WerkgeverAanpassen(Analyse analyse)
        {
            if (analyse == null)
            {
                return RedirectToAction("Index", "Home");
            }
            WerkgeverViewModel model = new WerkgeverViewModel(analyse.Departement);
            model.NaamAfdeling = analyse.Departement.Naam;
            model.Aanpassen = true;
            model.Titel = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(nameof(NieuweWerkgever), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public async Task<IActionResult> WerkgeverAanpassen(IFormFile file, WerkgeverViewModel model, Analyse analyse)
        {
            model.Aanpassen = true;

            model.Titel = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                return RedirectToAction("Index", "Home");
            }
            if (model.DepartementId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string origineelWerkgeverNaam;
            if (model.DepartementId != null)
            {
                origineelWerkgeverNaam =
                    _werkgeverRepository.GetDepartementById(model.DepartementId.Value).Werkgever.Naam;
            }
            else
            {
                origineelWerkgeverNaam = model.Naam;
            }
            if (ModelState.IsValid && 
                _analyseRepository.GetAllNietGearchiveerd(User.Identity.Name).Where(a => a.Departement.DepartementId != model.DepartementId).Any(a => ControlleerOfDepartementHetzelfdeIs(a.Departement, model, origineelWerkgeverNaam)))
            {
                ModelState.AddModelError("NaamAfdeling", "Er is al een actieve analyse voor deze dienst");
            }
            if (
                _werkgeverRepository.GetAll(User.Identity.Name)
                    .Where(
                        w =>
                            w.WerkgeverId !=
                            _werkgeverRepository.GetDepartementById(model.DepartementId.Value).Werkgever.WerkgeverId)
                    .Any(w => w.Naam.Equals(model.Naam)))
            {
                ModelState.AddModelError("NaamAfdeling", "Er is al een actieve analyse voor deze dienst");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Departement d = _werkgeverRepository.GetDepartementById(model.DepartementId.Value);
                    d.Werkgever.Naam = model.Naam;
                    d.Straat = model.Straat;
                    d.Nummer = model.Nummer;
                    d.Bus = model.Bus;
                    d.Postcode = model.Postcode;
                    d.Gemeente = model.Gemeente;
                    d.AantalWerkuren = model.AantalWerkuren;
                    d.Werkgever.PatronaleBijdrage = model.PatronaleBijdrage;
                    d.Naam = model.NaamAfdeling;
                    d.ContactPersoonNaam = model.ContactPersoonNaam;
                    d.ContactPersoonVoornaam = model.ContactPersoonVoornaam;
                    d.ContactPersoonEmail = model.ContactPersoonEmail;
                    analyse.Departement = d;
                    _werkgeverRepository.SaveChanges();
                    _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId).VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    
                    if (file != null && file.Length > 0)
                    {
                        var upload = Path.Combine(_environment.WebRootPath, "images\\uploads");
                        string[] gesplitst = file.FileName.Split('.');
                        string linkNaarLogoPrent = User.Identity.Name + "_" + analyse.Departement.Werkgever.WerkgeverId + "." +
                                                   gesplitst[gesplitst.Length - 1];
                        using (
                            var fileStream =
                                new FileStream(
                                    Path.Combine(upload, linkNaarLogoPrent),
                                    FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        _werkgeverRepository.GetById(analyse.Departement.Werkgever.WerkgeverId).LinkNaarLogoPrent =
                        "/images/uploads/" + linkNaarLogoPrent;
                        _werkgeverRepository.SaveChanges();
                    }
                    TempData["message"] = "De werkgever is succesvol aangepast";
                    return RedirectToAction(nameof(AnalyseOverzicht));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    TempData["error"] = "Er is iets fout gelopen, de werkgever werd niet aangepast";
                }
            }
            return View(nameof(NieuweWerkgever), model);
        }

        public IActionResult BestaandeWerkgever()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            BestaandeWerkgeverZoekenViewModel model = new BestaandeWerkgeverZoekenViewModel(_werkgeverRepository.GetAll(User.Identity.Name));
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1(Analyse analyse, AnalyseBaat1ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseBaat1ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat1Punt1(AnalyseBaat1ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Uren1 == null && model.Maandloon1 == null)
            {
                ModelState.AddModelError("VolgendeLijn1", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(3);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 3, KOBEnum.Baat, Formule.FormuleBaat3En4);
                        model.Lijst1 = new List<UrenMaandloonViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn1 == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn1);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Uren1 == null ? 0.ToString() : model.Uren1);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Maandloon1 == null ? 0.ToString() : model.Maandloon1);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(3));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(3));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat1));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat1ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat1Punt2(AnalyseBaat1ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Uren2 == null && model.Maandloon2 == null)
            {
                ModelState.AddModelError("VolgendeLijn2", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(4);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 4, KOBEnum.Baat, Formule.FormuleBaat3En4);
                        model.Lijst2 = new List<UrenMaandloonViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn2 == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn2);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Uren2 == null ? 0.ToString() : model.Uren2);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Maandloon2 == null ? 0.ToString() : model.Maandloon2);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(4));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(4));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat1));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat1ViewModelIn(analyse, model);

            model.ToonGroep2 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt1RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(3).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(3).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(3).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(3).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat1));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt2RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(4).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(4).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(4).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(4).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat1));
        }

        

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2(Analyse analyse, AnalyseBaat2ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt1(AnalyseBaat2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Beschrijving1 == null && model.Bedrag1 == null)
            {
                ModelState.AddModelError("VolgendeLijn1", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(5);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 5, KOBEnum.Baat, Formule.FormuleGeefVak2);
                        model.Lijst1 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn1 == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn1);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Beschrijving1 == null ? string.Empty : model.Beschrijving1);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Bedrag1 == null ? 0.ToString() : model.Bedrag1);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(5));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(5));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            model.ToonGroep1 = true;
            model.BevatFout = true;
            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt2(AnalyseBaat2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Beschrijving2 == null && model.Bedrag2 == null)
            {
                ModelState.AddModelError("VolgendeLijn2", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(9);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 9, KOBEnum.Baat, Formule.FormuleGeefVak2);
                        model.Lijst2 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn2 == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn2);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Beschrijving2 == null ? string.Empty : model.Beschrijving2);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Bedrag2 == null ? 0.ToString() : model.Bedrag2);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(9));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(9));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            model.ToonGroep2 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt3(AnalyseBaat2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(8);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 8, KOBEnum.Baat, Formule.FormuleGeefVak1);
                    }
                    KOBRij baatRij = baat.GeefKOBRijMetNummer(1);
                    if (baatRij == null)
                    {
                        baatRij = new KOBRij(baat, 1);
                    }

                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Baat8 == null ? 0.ToString() : model.Baat8);
                    baatRij.VulKOBVakIn(kostVak1);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(8));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(8));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt4(AnalyseBaat2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(10);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 10, KOBEnum.Baat, Formule.FormuleSomVak1En2);
                    }
                    KOBRij baatRij = baat.GeefKOBRijMetNummer(1);
                    if (baatRij == null)
                    {
                        baatRij = new KOBRij(baat, 1);
                    }

                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Baat10Punt1 == null ? 0.ToString() : model.Baat10Punt1);
                    KOBVak kostVak2 = baatRij.GeefKOBVakMetNummer(2);
                    if (kostVak2 == null)
                    {
                        kostVak2 = new KOBVak(baatRij, 2, 0.ToString());
                    }
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(10));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(10));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt5(AnalyseBaat2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(10);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 10, KOBEnum.Baat, Formule.FormuleSomVak1En2);
                    }
                    KOBRij baatRij = baat.GeefKOBRijMetNummer(1);
                    if (baatRij == null)
                    {
                        baatRij = new KOBRij(baat, 1);
                    }

                    KOBVak kostVak1 = baatRij.GeefKOBVakMetNummer(1);
                    if (kostVak1 == null)
                    {
                        kostVak1 = new KOBVak(baatRij, 1, 0.ToString());
                    }
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Baat10Punt2 == null ? 0.ToString() : model.Baat10Punt2);

                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(10));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(10));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseBaat2ViewModelIn(analyse, model);

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt1RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(5).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(5).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(5).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(5).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt2RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(9).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(9).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(9).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(9).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat3(Analyse analyse, AnalyseBaat3ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseBaat3ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt1(AnalyseBaat3ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Percent1 == null && model.Bedrag1 == null)
            {
                ModelState.AddModelError("VolgendeLijn1", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(6);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 6, KOBEnum.Baat, Formule.FormuleVermenigvuldigVak1En2);
                        model.Lijst1 = new List<BedragPercentViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn1 == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn1);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Bedrag1 == null ? 0.ToString() : model.Bedrag1);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Percent1 == null ? 0.ToString() : model.Percent1);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(6));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(6));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat3ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt2(AnalyseBaat3ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(7);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 7, KOBEnum.Baat, Formule.FormuleGeefVak1);
                    }
                    KOBRij baatRij = baat.GeefKOBRijMetNummer(1);
                    if (baatRij == null)
                    {
                        baatRij = new KOBRij(baat, 1);
                    }

                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Baat7 == null ? 0.ToString() : model.Baat7);
                    baatRij.VulKOBVakIn(kostVak1);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(7));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(7));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat3ViewModelIn(analyse, model);


            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt3(AnalyseBaat3ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(2);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 2, KOBEnum.Baat, Formule.FormuleGeefVak1);
                    }
                    KOBRij baatRij = baat.GeefKOBRijMetNummer(1);
                    if (baatRij == null)
                    {
                        baatRij = new KOBRij(baat, 1);
                    }

                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Baat2 == null ? 0.ToString() : model.Baat2);
                    baatRij.VulKOBVakIn(kostVak1);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(2));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(2));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat3ViewModelIn(analyse, model);


            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat3Punt1RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(6).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(6).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(6).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(6).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat4(AnalyseBaat4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseBaat4ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost, ActionName("AnalyseBaat4")]
        public IActionResult AnalyseBaat4Post(AnalyseBaat4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Beschrijving == null && model.Bedrag == null)
            {
                ModelState.AddModelError("VolgendeLijn", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat baat = analyse.GeefBaatMetNummer(11);
                    if (baat == null)
                    {
                        baat = new KostOfBaat(analyse, 11, KOBEnum.Baat, Formule.FormuleGeefVak2);
                        model.Lijst = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij baatRij;
                    if (model.VolgendeLijn == -1)
                    {
                        if (baat.Rijen.Count == 0)
                        {
                            baatRij = new KOBRij(baat, 1);
                        }
                        else
                        {
                            baatRij = new KOBRij(baat, baat.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        baatRij = baat.GeefKOBRijMetNummer(model.VolgendeLijn);
                    }
                    KOBVak kostVak1 = new KOBVak(baatRij, 1, model.Beschrijving == null ? string.Empty : model.Beschrijving);
                    KOBVak kostVak2 = new KOBVak(baatRij, 2, model.Bedrag == null ? 0.ToString() : model.Bedrag);
                    baatRij.VulKOBVakIn(kostVak1);
                    baatRij.VulKOBVakIn(kostVak2);
                    baat.VulKOBRijIn(baatRij);
                    analyse.SlaBaatMetNummerOp(baat);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(11));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(11));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseBaat4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseBaat4ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseBaat4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat4RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij baatrij = analyse.GeefBaatMetNummer(11).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij b = a.GeefBaatMetNummer(11).GeefKOBRijMetNummer(id);
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(11).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(11).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseBaat4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost(Analyse analyse, AnalyseKostViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseKost1ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost(AnalyseKostViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            if (model.Functie == null && model.AantalUrenPerWeek == null && model.BrutoMaandloonFulltime == null && model.Doelgroep.Equals("Kies uw doelgroep") && model.VlaamseOndersteuningsPremie.Equals("Vlaamse ondersteuningspremie") && model.AantalMaandenIBO == null && model.TotaleProductiviteitsPremieIBO == null)
            {
                ModelState.AddModelError("VolgendeLijn", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    KostOfBaat kost1 = analyse.GeefKostMetNummer(1);
                    KostOfBaat baat1 = analyse.GeefBaatMetNummer(1);

                    if (kost1 == null)
                    {
                        kost1 = new KostOfBaat(analyse, 1, KOBEnum.Kost, Formule.FormuleKost1);
                        model.LijnenKost1 = new List<AnalyseKostLijstObjectViewModel>();
                    }
                    if (baat1 == null)
                    {
                        baat1 = new KostOfBaat(analyse, 1, KOBEnum.Baat, Formule.FormuleBaat1);
                    }
                    KOBRij kostRij;
                    KOBRij baatRij;
                    if (model.VolgendeLijn == -1)
                    {
                        if (kost1.Rijen.Count == 0)
                        {
                            if (baat1.Rijen.Count == 0)
                            {
                                kostRij = new KOBRij(kost1, 1);
                                baatRij = new KOBRij(baat1, 1);
                            }
                            else
                            {
                                kostRij = new KOBRij(kost1, baat1.Rijen.Max(r => r.KOBRijId) + 1);
                                baatRij = new KOBRij(baat1, baat1.Rijen.Max(r => r.KOBRijId) + 1);
                            }
                        }
                        else
                        {
                            if (baat1.Rijen.Count == 0)
                            {
                                kostRij = new KOBRij(kost1, kost1.Rijen.Max(r => r.KOBRijId) + 1);
                                baatRij = new KOBRij(baat1, kost1.Rijen.Max(r => r.KOBRijId) + 1);
                            }
                            else
                            {
                                kostRij = new KOBRij(kost1,
                            Math.Max(kost1.Rijen.Max(r => r.KOBRijId) + 1, baat1.Rijen.Max(r => r.KOBRijId) + 1));
                                baatRij = new KOBRij(kost1,
                                    Math.Max(kost1.Rijen.Max(r => r.KOBRijId) + 1, baat1.Rijen.Max(r => r.KOBRijId) + 1));
                            }
                        }

                    }
                    else
                    {
                        kostRij = kost1.GeefKOBRijMetNummer(model.VolgendeLijn);
                        baatRij = baat1.GeefKOBRijMetNummer(model.VolgendeLijn);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Functie == null ? string.Empty : model.Functie);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.AantalUrenPerWeek == null ? 0.ToString() : model.AantalUrenPerWeek);
                    KOBVak kostVak3 = new KOBVak(kostRij, 3, model.BrutoMaandloonFulltime == null ? 0.ToString() : model.BrutoMaandloonFulltime);
                    KOBVak kostVak4 = new KOBVak(kostRij, 4, model.Doelgroep.Equals("Kies uw doelgroep") ? "ander" : model.Doelgroep);
                    KOBVak kostVak5 = new KOBVak(kostRij, 5, model.VlaamseOndersteuningsPremie.Equals("Vlaamse ondersteuningspremie") ? 0.ToString() : model.VlaamseOndersteuningsPremie.Substring(0, model.VlaamseOndersteuningsPremie.Length - 1));
                    KOBVak baatVak1 = new KOBVak(baatRij, 1, model.AantalMaandenIBO == null ? 0.ToString() : model.AantalMaandenIBO);
                    KOBVak baatVak2 = new KOBVak(baatRij, 2, model.TotaleProductiviteitsPremieIBO == null ? 0.ToString() : model.TotaleProductiviteitsPremieIBO);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kostRij.VulKOBVakIn(kostVak3);
                    kostRij.VulKOBVakIn(kostVak4);
                    kostRij.VulKOBVakIn(kostVak5);
                    baatRij.VulKOBVakIn(baatVak1);
                    baatRij.VulKOBVakIn(baatVak2);
                    kost1.VulKOBRijIn(kostRij);
                    baat1.VulKOBRijIn(baatRij);
                    analyse.SlaKostMetNummerOp(kost1);
                    analyse.SlaBaatMetNummerOp(baat1);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefBaatMetNummer(1));
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(1));
                    _analyseRepository.SaveChanges();
                    a.SlaBaatMetNummerOp(analyse.GeefBaatMetNummer(1));
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(1));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseKost1ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKostRijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(1).GeefKOBRijMetNummer(id);
            KOBRij baatrij = analyse.GeefBaatMetNummer(1).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(1).GeefKOBRijMetNummer(id);
            KOBRij b = a.GeefBaatMetNummer(1).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(1).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(1).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(1).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(1).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost2(Analyse analyse, AnalyseKost2ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseKost2ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost2(AnalyseKost2ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            if (model.Bedrag == null && model.Beschrijving == null)
            {
                ModelState.AddModelError("VolgendeLijn", "Gelieve minstens 1 veld in te vullen.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    KostOfBaat kost = analyse.GeefKostMetNummer(8);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 8, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving == null ? string.Empty : model.Beschrijving);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag == null ? 0.ToString() : model.Bedrag);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(8));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(8));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();

                    return RedirectToAction(nameof(AnalyseKost2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseKost2ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost2RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(8).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(8).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(8).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(8).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3(Analyse analyse, AnalyseKost3ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseKost3ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost3Punt1(AnalyseKost3ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Bedrag1 == null && model.Beschrijving1 == null)
            {
                ModelState.AddModelError("VolgendeLijn1", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(3);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 3, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst1 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn1 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn1);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving1 == null ? string.Empty : model.Beschrijving1);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag1 == null ? 0.ToString() : model.Bedrag1);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(3));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(3));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseKost3ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost3Punt2(AnalyseKost3ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Bedrag2 == null && model.Beschrijving2 == null)
            {
                ModelState.AddModelError("VolgendeLijn2", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(4);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 4, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst2 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn2 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn2);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving2 == null ? string.Empty : model.Beschrijving2);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag2 == null ? 0.ToString() : model.Bedrag2);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(4));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(4));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            VulAnalyseKost3ViewModelIn(analyse, model);

            model.ToonGroep2 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt1RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(3).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(3).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(3).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(3).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt2RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(4).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(4).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(4).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(4).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4(Analyse analyse, AnalyseKost4ViewModel model)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            
            VulAnalyseKost4ViewModelIn(analyse, model);

            ViewData["werkgever"] = analyse.Departement.Werkgever.Naam + " - " + analyse.Departement.Naam;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt1(AnalyseKost4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Bedrag1 == null && model.Beschrijving1 == null)
            {
                ModelState.AddModelError("VolgendeLijn1", "Gelieve minstens 1 veld in te vullen.");
            }

            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(2);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 2, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst1 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn1 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn1);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving1 == null ? string.Empty : model.Beschrijving1);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag1 == null ? 0.ToString() : model.Bedrag1);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(2));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(2));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseKost4ViewModelIn(analyse, model);

            model.ToonGroep1 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt2(AnalyseKost4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Uren2 == null && model.Maandloon2 == null)
            {
                ModelState.AddModelError("VolgendeLijn2", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(6);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 6, KOBEnum.Kost, Formule.FormuleKost6);
                        model.Lijst2 = new List<UrenMaandloonViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn2 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn2);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Uren2 == null ? 0.ToString() : model.Uren2);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Maandloon2 == null ? 0.ToString() : model.Maandloon2);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(6));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(6));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseKost4ViewModelIn(analyse, model);

            model.ToonGroep2 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt3(AnalyseKost4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Beschrijving3 == null && model.Bedrag3 == null)
            {
                ModelState.AddModelError("VolgendeLijn3", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(5);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 5, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst3 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn3 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn3);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving3 == null ? string.Empty : model.Beschrijving3);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag3 == null ? 0.ToString() : model.Bedrag3);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(5));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(5));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseKost4ViewModelIn(analyse, model);

            model.ToonGroep3 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt4(AnalyseKost4ViewModel model, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            if (model.Beschrijving4 == null && model.Bedrag4 == null)
            {
                ModelState.AddModelError("VolgendeLijn4", "Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    KostOfBaat kost = analyse.GeefKostMetNummer(7);
                    if (kost == null)
                    {
                        kost = new KostOfBaat(analyse, 7, KOBEnum.Kost, Formule.FormuleGeefVak2);
                        model.Lijst4 = new List<BeschrijvingBedragViewModel>();
                    }
                    KOBRij kostRij;
                    if (model.VolgendeLijn4 == -1)
                    {
                        if (kost.Rijen.Count == 0)
                        {
                            kostRij = new KOBRij(kost, 1);
                        }
                        else
                        {
                            kostRij = new KOBRij(kost, kost.Rijen.Max(r => r.KOBRijId) + 1);
                        }
                    }
                    else
                    {
                        kostRij = kost.GeefKOBRijMetNummer(model.VolgendeLijn4);
                    }
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving4 == null ? string.Empty : model.Beschrijving4);
                    KOBVak kostVak2 = new KOBVak(kostRij, 2, model.Bedrag4 == null ? 0.ToString() : model.Bedrag4);
                    kostRij.VulKOBVakIn(kostVak1);
                    kostRij.VulKOBVakIn(kostVak2);
                    kost.VulKOBRijIn(kostRij);
                    analyse.SlaKostMetNummerOp(kost);

                    Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
                    a.KostenEnBaten.Remove(a.GeefKostMetNummer(7));
                    _analyseRepository.SaveChanges();
                    a.SlaKostMetNummerOp(analyse.GeefKostMetNummer(7));
                    _analyseRepository.SaveChanges();
                    a.VernieuwDatum();
                    _analyseRepository.SaveChanges();
                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            VulAnalyseKost4ViewModelIn(analyse, model);

            model.ToonGroep4 = true;

            model.BevatFout = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt1RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(2).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(2).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(2).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(2).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt2RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(6).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(6).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(6).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(6).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt3RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(5).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(5).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(5).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(5).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt4RijVerwijderen(int id, Analyse analyse)
        {
            AnalyseFilter.HaalMailsUitSessie(HttpContext);
            if (ControleerOfSessieVerlopenIs(analyse))
            {
                TempData["error"] = "Uw sessie is verlopen, u kan vanaf hier verder werken";
                return RedirectToAction("Index", "Home");
            }
            KOBRij kostrij = analyse.GeefKostMetNummer(7).GeefKOBRijMetNummer(id);
            Analyse a = _analyseRepository.GetById(User.Identity.Name, analyse.AnalyseId);
            KOBRij k = a.GeefKostMetNummer(7).GeefKOBRijMetNummer(id);
            if (kostrij != null)
            {
                analyse.GeefKostMetNummer(7).VerwijderKOBRij(kostrij);
            }
            if (k != null)
            {
                a.GeefKostMetNummer(7).VerwijderKOBRij(k);
                _analyseRepository.SaveChanges();
                a.VernieuwDatum();
                _analyseRepository.SaveChanges();
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        private void VulAnalyseBaat1ViewModelIn(Analyse analyse, AnalyseBaat1ViewModel model)
        {
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(3);
            int hoogsteId1;
            if (baat1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (baat1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = baat1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<UrenMaandloonViewModel> lijst1 = new List<UrenMaandloonViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij baatRij;
                if (baat1 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat1.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    UrenMaandloonViewModel m = new UrenMaandloonViewModel()
                    {
                        KostId = i,
                        Uren = (int)baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble(),
                        Maandloon = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst1 = lijst1;
            model.AantalLijnenLijst1 = model.Lijst1.Count;

            KostOfBaat baat2 = analyse.GeefBaatMetNummer(4);
            int hoogsteId2;
            if (baat2 == null)
            {
                hoogsteId2 = 0;
            }
            else
            {
                if (baat2.Rijen.Count == 0)
                {
                    hoogsteId2 = 0;
                }
                else
                {
                    hoogsteId2 = baat2.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<UrenMaandloonViewModel> lijst2 = new List<UrenMaandloonViewModel>();
            for (var i = 1; i <= hoogsteId2; i++)
            {
                KOBRij baatRij;
                if (baat2 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat2.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    UrenMaandloonViewModel m = new UrenMaandloonViewModel()
                    {
                        KostId = i,
                        Uren = (int)baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble(),
                        Maandloon = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst2.Add(m);
                }

            }


            model.Lijst2 = lijst2;
            model.AantalLijnenLijst2 = model.Lijst2.Count;
        }

        public void VulAnalyseBaat2ViewModelIn(Analyse analyse, AnalyseBaat2ViewModel model)
        {
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(5);
            int hoogsteId1;
            if (baat1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (baat1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = baat1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst1 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij baatRij;
                if (baat1 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat1.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
                    {
                        KostId = i,
                        Beschrijving = baatRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst1 = lijst1;
            model.AantalLijnenLijst1 = model.Lijst1.Count;

            KostOfBaat baat2 = analyse.GeefBaatMetNummer(9);
            int hoogsteId2;
            if (baat2 == null)
            {
                hoogsteId2 = 0;
            }
            else
            {
                if (baat2.Rijen.Count == 0)
                {
                    hoogsteId2 = 0;
                }
                else
                {
                    hoogsteId2 = baat2.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst2 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId2; i++)
            {
                KOBRij baatRij;
                if (baat2 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat2.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
                    {
                        KostId = i,
                        Beschrijving = baatRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst2.Add(m);
                }

            }


            model.Lijst2 = lijst2;
            model.AantalLijnenLijst2 = model.Lijst2.Count;


            KostOfBaat baat3 = analyse.GeefBaatMetNummer(8);
            if (baat3 != null)
            {
                KOBRij rij = baat3.GeefKOBRijMetNummer(1);
                if (rij != null)
                {
                    model.Baat8 = Convert.ToDouble(rij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()) == 0 ? null : rij.GeefKOBVakMetNummer(1).Data;
                }
            }

            KostOfBaat baat4 = analyse.GeefBaatMetNummer(10);
            if (baat4 != null)
            {
                KOBRij rij = baat4.GeefKOBRijMetNummer(1);
                if (rij != null)
                {
                    model.Baat10Punt1 = Convert.ToDouble(rij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()) == 0 ? null : rij.GeefKOBVakMetNummer(1).Data;
                    model.Baat10Punt2 = Convert.ToDouble(rij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()) == 0 ? null : rij.GeefKOBVakMetNummer(2).Data;
                }
            }
        }

        public void VulAnalyseBaat3ViewModelIn(Analyse analyse, AnalyseBaat3ViewModel model)
        {
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(6);
            int hoogsteId1;
            if (baat1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (baat1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = baat1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BedragPercentViewModel> lijst1 = new List<BedragPercentViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij baatRij;
                if (baat1 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat1.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    BedragPercentViewModel m = new BedragPercentViewModel()
                    {
                        KostId = i,
                        Bedrag = baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble(),
                        Percent = (int)baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst1 = lijst1;
            model.AantalLijnenLijst1 = model.Lijst1.Count;


            KostOfBaat baat2 = analyse.GeefBaatMetNummer(7);
            if (baat2 != null)
            {
                KOBRij rij = baat2.GeefKOBRijMetNummer(1);
                if (rij != null)
                {
                    model.Baat7 = Convert.ToDouble(rij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()) == 0 ? null : rij.GeefKOBVakMetNummer(1).Data;
                }
            }

            KostOfBaat baat3 = analyse.GeefBaatMetNummer(2);
            if (baat3 != null)
            {
                KOBRij rij = baat3.GeefKOBRijMetNummer(1);
                if (rij != null)
                {
                    model.Baat2 = Convert.ToDouble(rij.GeefKOBVakMetNummer(1).GeefDataAlsDouble()) == 0 ? null : rij.GeefKOBVakMetNummer(1).Data;
                }
            }
        }

        public void VulAnalyseBaat4ViewModelIn(Analyse analyse, AnalyseBaat4ViewModel model)
        {
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(11);
            int hoogsteId1;
            if (baat1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (baat1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = baat1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst1 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij baatRij;
                if (baat1 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat1.GeefKOBRijMetNummer(i);
                }

                if (baatRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
                    {
                        KostId = i,
                        Beschrijving = baatRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst = lijst1;
            model.AantalLijnenLijst = model.Lijst.Count;
        }

        public void VulAnalyseKost1ViewModelIn(Analyse analyse, AnalyseKostViewModel model)
        {
            KostOfBaat kost1 = analyse.GeefKostMetNummer(1);
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(1);
            int hoogsteId;
            if (kost1 == null && baat1 == null)
            {
                hoogsteId = 0;
            }
            else
            {
                if (kost1 == null)
                {
                    if (baat1.Rijen.Count == 0)
                    {
                        hoogsteId = 0;
                    }
                    else
                    {
                        hoogsteId = baat1.Rijen.Max(r => r.KOBRijId);
                    }
                }
                else if (baat1 == null)
                {
                    if (kost1.Rijen.Count == 0)
                    {
                        hoogsteId = 0;
                    }
                    else
                    {
                        hoogsteId = kost1.Rijen.Max(r => r.KOBRijId);
                    }
                }
                else
                {
                    if (kost1.Rijen.Count == 0)
                    {
                        if (baat1.Rijen.Count == 0)
                        {
                            hoogsteId = 0;
                        }
                        else
                        {
                            hoogsteId = baat1.Rijen.Max(r => r.KOBRijId);
                        }
                    }
                    else
                    {
                        if (baat1.Rijen.Count == 0)
                        {
                            hoogsteId = kost1.Rijen.Max(r => r.KOBRijId);
                        }
                        else
                        {
                            hoogsteId = Math.Max(kost1.Rijen.Max(r => r.KOBRijId), baat1.Rijen.Max(r => r.KOBRijId));
                        }
                    }
                }
            }
            IList<AnalyseKostLijstObjectViewModel> lijst = new List<AnalyseKostLijstObjectViewModel>();
            Resultaat resultaat = new Resultaat(_doelgroepRepository.GetAll());
            if (kost1 != null)
            {
                resultaat.GeefParametersDoor(kost1, analyse);
                foreach (KOBRij rij in kost1.Rijen)
                {
                    resultaat.BerekenEnSetResultaat(rij);
                }
            }
            if (baat1 != null)
            {

                resultaat.GeefParametersDoor(baat1, analyse);
                foreach (KOBRij rij in baat1.Rijen)
                {
                    resultaat.BerekenEnSetResultaat(rij);
                }
            }
            for (var i = 1; i <= hoogsteId; i++)
            {
                KOBRij kostRij;
                KOBRij baatRij;
                if (kost1 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost1.GeefKOBRijMetNummer(i);
                }
                if (baat1 == null)
                {
                    baatRij = null;
                }
                else
                {
                    baatRij = baat1.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null || baatRij != null)
                {
                    AnalyseKostLijstObjectViewModel m = new AnalyseKostLijstObjectViewModel();
                    if (kostRij != null)
                    {
                        m.Kost1Id = i;
                        m.Functie = kostRij.GeefKOBVakMetNummer(1).Data;
                        m.AantalUrenPerWeek = (int)kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                        m.BrutoMaandloonFulltime = kostRij.GeefKOBVakMetNummer(3).GeefDataAlsDouble();
                        m.Doelgroep = kostRij.GeefKOBVakMetNummer(4).Data;
                        m.VlaamseOndersteuningsPremie = (int)kostRij.GeefKOBVakMetNummer(5).GeefDataAlsDouble();
                        m.KostUitkomst = kostRij.Resultaat * 12;
                    }
                    if (baatRij != null)
                    {
                        m.Kost1Id = i;
                        m.AantalMaandenIBO = (int)baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble();
                        m.TotaleProductiviteitsPremieIBO = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                        m.BaatUitkomst = baatRij.Resultaat;
                    }
                    lijst.Add(m);
                }

            }
            model.Doelgroepen = _doelgroepRepository.GetAllSelecteerbaar().Select(d => d.DoelgroepText).ToList();


            model.AantalLijnenKost1 = lijst.Count;
            model.LijnenKost1 = lijst;
        }

        public void VulAnalyseKost2ViewModelIn(Analyse analyse, AnalyseKost2ViewModel model)
        {
            KostOfBaat kost1 = analyse.GeefKostMetNummer(8);
            int hoogsteId;
            if (kost1 == null)
            {
                hoogsteId = 0;
            }
            else
            {
                if (kost1.Rijen.Count == 0)
                {
                    hoogsteId = 0;
                }
                else
                {
                    hoogsteId = kost1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId; i++)
            {
                KOBRij kostRij;
                if (kost1 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost1.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel();
                    m.KostId = i;
                    m.Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data;
                    m.Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();

                    lijst.Add(m);
                }

            }


            model.Lijst = lijst;
            model.AantalLijnenLijst = model.Lijst.Count;
        }

        public void VulAnalyseKost3ViewModelIn(Analyse analyse, AnalyseKost3ViewModel model)
        {
            KostOfBaat kost1 = analyse.GeefKostMetNummer(3);
            int hoogsteId1;
            if (kost1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (kost1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = kost1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst1 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij kostRij;
                if (kost1 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost1.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel
                    {
                        KostId = i,
                        Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst1 = lijst1;
            model.AantalLijnenLijst1 = model.Lijst1.Count;

            KostOfBaat kost2 = analyse.GeefKostMetNummer(4);
            int hoogsteId2;
            if (kost2 == null)
            {
                hoogsteId2 = 0;
            }
            else
            {
                if (kost2.Rijen.Count == 0)
                {
                    hoogsteId2 = 0;
                }
                else
                {
                    hoogsteId2 = kost2.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst2 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId2; i++)
            {
                KOBRij kostRij;
                if (kost2 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost2.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel
                    {
                        KostId = i,
                        Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst2.Add(m);
                }

            }


            model.Lijst2 = lijst2;
            model.AantalLijnenLijst2 = model.Lijst2.Count;
        }

        public void VulAnalyseKost4ViewModelIn(Analyse analyse, AnalyseKost4ViewModel model)
        {
            KostOfBaat kost1 = analyse.GeefKostMetNummer(2);
            int hoogsteId1;
            if (kost1 == null)
            {
                hoogsteId1 = 0;
            }
            else
            {
                if (kost1.Rijen.Count == 0)
                {
                    hoogsteId1 = 0;
                }
                else
                {
                    hoogsteId1 = kost1.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst1 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId1; i++)
            {
                KOBRij kostRij;
                if (kost1 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost1.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel
                    {
                        KostId = i,
                        Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst1.Add(m);
                }

            }


            model.Lijst1 = lijst1;
            model.AantalLijnenLijst1 = model.Lijst1.Count;

            KostOfBaat kost2 = analyse.GeefKostMetNummer(6);
            int hoogsteId2;
            if (kost2 == null)
            {
                hoogsteId2 = 0;
            }
            else
            {
                if (kost2.Rijen.Count == 0)
                {
                    hoogsteId2 = 0;
                }
                else
                {
                    hoogsteId2 = kost2.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<UrenMaandloonViewModel> lijst2 = new List<UrenMaandloonViewModel>();
            for (var i = 1; i <= hoogsteId2; i++)
            {
                KOBRij kostRij;
                if (kost2 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost2.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    UrenMaandloonViewModel m = new UrenMaandloonViewModel()
                    {
                        KostId = i,
                        Uren = (int)kostRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble(),
                        Maandloon = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst2.Add(m);
                }

            }


            model.Lijst2 = lijst2;
            model.AantalLijnenLijst2 = model.Lijst2.Count;


            KostOfBaat kost3 = analyse.GeefKostMetNummer(5);
            int hoogsteId3;
            if (kost3 == null)
            {
                hoogsteId3 = 0;
            }
            else
            {
                if (kost3.Rijen.Count == 0)
                {
                    hoogsteId3 = 0;
                }
                else
                {
                    hoogsteId3 = kost3.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst3 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId3; i++)
            {
                KOBRij kostRij;
                if (kost3 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost3.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel
                    {
                        KostId = i,
                        Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst3.Add(m);
                }

            }


            model.Lijst3 = lijst3;
            model.AantalLijnenLijst3 = model.Lijst3.Count;

            KostOfBaat kost4 = analyse.GeefKostMetNummer(7);
            int hoogsteId4;
            if (kost4 == null)
            {
                hoogsteId4 = 0;
            }
            else
            {
                if (kost4.Rijen.Count == 0)
                {
                    hoogsteId4 = 0;
                }
                else
                {
                    hoogsteId4 = kost4.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst4 = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId4; i++)
            {
                KOBRij kostRij;
                if (kost4 == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost4.GeefKOBRijMetNummer(i);
                }

                if (kostRij != null)
                {
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel
                    {
                        KostId = i,
                        Beschrijving = kostRij.GeefKOBVakMetNummer(1).Data,
                        Bedrag = kostRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble()
                    };

                    lijst4.Add(m);
                }

            }


            model.Lijst4 = lijst4;
            model.AantalLijnenLijst4 = model.Lijst4.Count;
        }

        public bool ControleerOfSessieVerlopenIs(Analyse analyse)
        {
            return (analyse == null) || (analyse.JobCoachEmail == null && analyse.KostenEnBaten == null && analyse.Departement == null);
        }

        public bool ControlleerOfDepartementHetzelfdeIs(Departement d, WerkgeverViewModel model, string origineelWerkgeverNaam)
        {
            return d.Werkgever.Naam.ToLower().Equals(origineelWerkgeverNaam.ToLower()) && d.Naam.ToLower().Equals(model.NaamAfdeling.ToLower()) &&
                   d.Postcode.Equals(model.Postcode) && d.Straat.ToLower().Equals(model.Straat.ToLower()) &&
                   d.Gemeente.ToLower().Equals(model.Gemeente.ToLower()) && d.Nummer.Equals(model.Nummer);
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

