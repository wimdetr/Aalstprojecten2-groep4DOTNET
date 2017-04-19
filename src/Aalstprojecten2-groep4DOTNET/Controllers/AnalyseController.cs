using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Filters;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.AnalyseViewModels;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    //[ServiceFilter(typeof(AnalyseFilter))]
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

        public IActionResult PasAnalyseAan(int id = -1)
        {
            Analyse a = _analyseRepository.GetById(User.Identity.Name, id);
            AnalyseFilter.PlaatsAnalyseInSession(a, HttpContext);
            return RedirectToAction(nameof(AnalyseOverzicht));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseOverzicht(Analyse analyse)
        {
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
            var model = werkgever == null ? new WerkgeverViewModel() : new WerkgeverViewModel(werkgever);

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
                    Werkgever w = new Werkgever(a, model.Naam, model.Postcode, model.Gemeente, model.NaamAfdeling)
                    {
                        Straat = model.Straat,
                        Nummer = model.Nummer,
                        Bus = model.Bus,
                        AantalWerkuren = model.AantalWerkuren,
                        PatronaleBijdrage = model.PatronaleBijdrage,
                        LinkNaarLogoPrent = model.LinkNaarLogoPrent,
                        ContactPersoonNaam = model.ContactPersoonNaam,
                        ContactPersoonVoornaam = model.ContactPersoonVoornaam,
                        ContactPersoonEmail = model.ContactPersoonEmail
                    };
                    a.Werkgever = w;
                    _analyseRepository.Add(a);
                    _analyseRepository.SaveChanges();
                    AnalyseFilter.PlaatsAnalyseInSession(a, HttpContext);
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

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1(Analyse analyse, AnalyseBaat1ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat1Punt1(AnalyseBaat1ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat1));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

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


            model.ToonGroep1 = true;
            return View(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat1Punt2(AnalyseBaat1ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat1));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }


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


            model.ToonGroep2 = true;
            return View(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt1RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(3).GeefKOBRijMetNummer(id);
            AnalyseBaat1ViewModel model = new AnalyseBaat1ViewModel();
            if (baatrij != null)
            {
                model.Uren1 = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Maandloon1 = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Uren1 = Convert.ToInt16(model.Uren1) == 0 ? null : model.Uren1;
            model.Maandloon1 = Convert.ToDouble(model.Maandloon1) == 0 ? null : model.Maandloon1;
            model.VolgendeLijn1 = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt1RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat1));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt2RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(4).GeefKOBRijMetNummer(id);
            AnalyseBaat1ViewModel model = new AnalyseBaat1ViewModel();
            if (baatrij != null)
            {
                model.Uren2 = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Maandloon2 = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Uren2 = Convert.ToInt16(model.Uren2) == 0 ? null : model.Uren2;
            model.Maandloon2 = Convert.ToDouble(model.Maandloon2) == 0 ? null : model.Maandloon2;
            model.VolgendeLijn2 = id;
            model.ToonGroep2 = true;
            return RedirectToAction(nameof(AnalyseBaat1), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat1Punt2RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat1));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2(Analyse analyse, AnalyseBaat2ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt1(AnalyseBaat2ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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


            model.ToonGroep1 = true;
            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt2(AnalyseBaat2ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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


            model.ToonGroep2 = true;
            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt3(AnalyseBaat2ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt4(AnalyseBaat2ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat2Punt5(AnalyseBaat2ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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

            return View(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt1RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(5).GeefKOBRijMetNummer(id);
            AnalyseBaat2ViewModel model = new AnalyseBaat2ViewModel();
            if (baatrij != null)
            {
                model.Beschrijving1 = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag1 = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag1 = Convert.ToDouble(model.Bedrag1) == 0 ? null : model.Bedrag1;
            model.VolgendeLijn1 = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt1RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt2RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(9).GeefKOBRijMetNummer(id);
            AnalyseBaat2ViewModel model = new AnalyseBaat2ViewModel();
            if (baatrij != null)
            {
                model.Beschrijving2 = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag2 = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag2 = Convert.ToDouble(model.Bedrag2) == 0 ? null : model.Bedrag2;
            model.VolgendeLijn2 = id;
            model.ToonGroep2 = true;
            return RedirectToAction(nameof(AnalyseBaat2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat2Punt2RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat3(Analyse analyse, AnalyseBaat3ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt1(AnalyseBaat3ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

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

            model.ToonGroep1 = true;
            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt2(AnalyseBaat3ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

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


            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseBaat3Punt3(AnalyseBaat3ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

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


            return View(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat3Punt1RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(6).GeefKOBRijMetNummer(id);
            AnalyseBaat3ViewModel model = new AnalyseBaat3ViewModel();
            if (baatrij != null)
            {
                model.Bedrag1 = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Percent1 = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag1 = Convert.ToDouble(model.Bedrag1) == 0 ? null : model.Bedrag1;
            model.Percent1 = Convert.ToInt16(model.Percent1) == 0 ? null : model.Percent1;
            model.VolgendeLijn1 = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseBaat3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat3Punt1RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat4(AnalyseBaat4ViewModel model, Analyse analyse)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost, ActionName("AnalyseBaat4")]
        public IActionResult AnalyseBaat4Post(AnalyseBaat4ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseBaat4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

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

            model.ToonGroep1 = true;
            return View(nameof(AnalyseBaat4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat4RijAanpassen(int id, Analyse analyse)
        {
            KOBRij baatrij = analyse.GeefBaatMetNummer(11).GeefKOBRijMetNummer(id);
            AnalyseBaat4ViewModel model = new AnalyseBaat4ViewModel();
            if (baatrij != null)
            {
                model.Beschrijving = baatrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag = Convert.ToDouble(model.Bedrag) == 0 ? null : model.Bedrag;
            model.VolgendeLijn = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseBaat4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseBaat4RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseBaat4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost(Analyse analyse, AnalyseKostViewModel model)
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
                    }
                    if (baatRij != null)
                    {
                        m.Kost1Id = i;
                        m.AantalMaandenIBO = (int)baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble();
                        m.TotaleProductiviteitsPremieIBO = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    }
                    lijst.Add(m);
                }

            }
            model.AantalLijnenKost1 = lijst.Count;
            model.LijnenKost1 = lijst;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost(AnalyseKostViewModel model, Analyse analyse)
        {
            KostOfBaat kost1 = analyse.GeefKostMetNummer(1);
            KostOfBaat baat1 = analyse.GeefBaatMetNummer(1);
            if (model.Functie == null && model.AantalUrenPerWeek == null && model.BrutoMaandloonFulltime == null && model.Doelgroep.Equals("Kies uw doelgroep") && model.VlaamseOndersteuningsPremie.Equals("Vlaamse ondersteuningspremie") && model.AantalMaandenIBO == null && model.TotaleProductiviteitsPremieIBO == null)
            {
                ModelState.AddModelError("VolgendeLijn","Gelieve minstens 1 veld in te vullen.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    

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
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Functie == null? string.Empty : model.Functie);
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

                    return RedirectToAction(nameof(AnalyseKost));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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
                    }
                    if (baatRij != null)
                    {
                        m.Kost1Id = i;
                        m.AantalMaandenIBO = (int)baatRij.GeefKOBVakMetNummer(1).GeefDataAlsDouble();
                        m.TotaleProductiviteitsPremieIBO = baatRij.GeefKOBVakMetNummer(2).GeefDataAlsDouble();
                    }
                    lijst.Add(m);
                }

            }
            model.AantalLijnenKost1 = lijst.Count;
            model.LijnenKost1 = lijst;

            model.ToonGroep1 = true;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKostRijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(1).GeefKOBRijMetNummer(id);
            KOBRij baatrij = analyse.GeefBaatMetNummer(1).GeefKOBRijMetNummer(id);
            AnalyseKostViewModel model = new AnalyseKostViewModel();
            if (kostrij != null)
            {
                model.Functie = kostrij.GeefKOBVakMetNummer(1).Data;
                model.AantalUrenPerWeek = kostrij.GeefKOBVakMetNummer(2).Data;
                model.BrutoMaandloonFulltime = kostrij.GeefKOBVakMetNummer(3).Data;
                model.Doelgroep = kostrij.GeefKOBVakMetNummer(4).Data;
                model.VlaamseOndersteuningsPremie = kostrij.GeefKOBVakMetNummer(5).Data + "%";
            }
            if (baatrij != null)
            {
                model.AantalMaandenIBO = baatrij.GeefKOBVakMetNummer(1).Data;
                model.TotaleProductiviteitsPremieIBO = baatrij.GeefKOBVakMetNummer(2).Data;
            }
            model.AantalUrenPerWeek = Convert.ToInt16(model.AantalUrenPerWeek) == 0 ? null : model.AantalUrenPerWeek;
            model.BrutoMaandloonFulltime = Convert.ToDouble(model.BrutoMaandloonFulltime) == 0 ? null : model.BrutoMaandloonFulltime;
            model.AantalMaandenIBO = Convert.ToInt16(model.AantalMaandenIBO) == 0 ? null : model.AantalMaandenIBO;
            model.TotaleProductiviteitsPremieIBO = Convert.ToDouble(model.TotaleProductiviteitsPremieIBO) == 0 ? null : model.TotaleProductiviteitsPremieIBO;
            model.VolgendeLijn = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseKost), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKostRijVerwijderen(int id, Analyse analyse)
        {
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
            }
            if (baatrij != null)
            {
                analyse.GeefBaatMetNummer(1).VerwijderKOBRij(baatrij);
            }
            if (b != null)
            {
                a.GeefBaatMetNummer(1).VerwijderKOBRij(b);
                _analyseRepository.SaveChanges();
            }
            
            return RedirectToAction(nameof(AnalyseKost));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost2(Analyse analyse, AnalyseKost2ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost2(AnalyseKost2ViewModel model, Analyse analyse)
        {
            KostOfBaat kost = analyse.GeefKostMetNummer(8);
            if (model.Bedrag == null && model.Beschrijving == null)
            {
                ModelState.AddModelError("VolgendeLijn", "Gelieve minstens 1 veld in te vullen.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
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
                    KOBVak kostVak1 = new KOBVak(kostRij, 1, model.Beschrijving == null? string.Empty:model.Beschrijving);
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

                    return RedirectToAction(nameof(AnalyseKost2));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            int hoogsteId;
            if (kost == null)
            {
                hoogsteId = 0;
            }
            else
            {
                if (kost.Rijen.Count == 0)
                {
                    hoogsteId = 0;
                }
                else
                {
                    hoogsteId = kost.Rijen.Max(r => r.KOBRijId);
                }
            }
            IList<BeschrijvingBedragViewModel> lijst = new List<BeschrijvingBedragViewModel>();
            for (var i = 1; i <= hoogsteId; i++)
            {
                KOBRij kostRij;
                if (kost == null)
                {
                    kostRij = null;
                }
                else
                {
                    kostRij = kost.GeefKOBRijMetNummer(i);
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

            model.ToonGroep1 = true;
            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost2RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(8).GeefKOBRijMetNummer(id);
            AnalyseKost2ViewModel model = new AnalyseKost2ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag = Convert.ToDouble(model.Bedrag) == 0 ? null : model.Bedrag;
            model.VolgendeLijn = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseKost2), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost2RijVerwijderen(int id, Analyse analyse)
        {
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
            }
            
            return RedirectToAction(nameof(AnalyseKost2));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3(Analyse analyse, AnalyseKost3ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost3Punt1(AnalyseKost3ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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

            model.ToonGroep1 = true;
            return View(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost3Punt2(AnalyseKost3ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost3));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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

            model.ToonGroep2 = true;
            return View(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt1RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(3).GeefKOBRijMetNummer(id);
            AnalyseKost3ViewModel model = new AnalyseKost3ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving1 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag1 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag1 = Convert.ToDouble(model.Bedrag1) == 0 ? null : model.Bedrag1;
            model.VolgendeLijn1 = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt1RijVerwijderen(int id, Analyse analyse)
        {
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
            }
            
            return RedirectToAction(nameof(AnalyseKost3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt2RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(4).GeefKOBRijMetNummer(id);
            AnalyseKost3ViewModel model = new AnalyseKost3ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving2 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag2 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag2 = Convert.ToDouble(model.Bedrag2) == 0 ? null : model.Bedrag2;
            model.VolgendeLijn2 = id;
            model.ToonGroep2 = true;
            return RedirectToAction(nameof(AnalyseKost3), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost3Punt2RijVerwijderen(int id, Analyse analyse)
        {
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
            }
            
            return RedirectToAction(nameof(AnalyseKost3));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4(Analyse analyse, AnalyseKost4ViewModel model)
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

            return View(model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt1(AnalyseKost4ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
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

            model.ToonGroep1 = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt2(AnalyseKost4ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
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

            model.ToonGroep2 = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt3(AnalyseKost4ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
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

            model.ToonGroep3 = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        [HttpPost]
        public IActionResult AnalyseKost4Punt4(AnalyseKost4ViewModel model, Analyse analyse)
        {
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

                    return RedirectToAction(nameof(AnalyseKost4));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
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
                    BeschrijvingBedragViewModel m = new BeschrijvingBedragViewModel()
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

            model.ToonGroep4 = true;
            return View(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt1RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(2).GeefKOBRijMetNummer(id);
            AnalyseKost4ViewModel model = new AnalyseKost4ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving1 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag1 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag1 = Convert.ToDouble(model.Bedrag1) == 0 ? null : model.Bedrag1;
            model.VolgendeLijn1 = id;
            model.ToonGroep1 = true;
            return RedirectToAction(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt1RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt2RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(6).GeefKOBRijMetNummer(id);
            AnalyseKost4ViewModel model = new AnalyseKost4ViewModel();
            if (kostrij != null)
            {
                model.Uren2 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Maandloon2 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Maandloon2 = Convert.ToDouble(model.Maandloon2) == 0 ? null : model.Maandloon2;
            model.VolgendeLijn2 = id;
            model.ToonGroep2 = true;
            return RedirectToAction(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt2RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt3RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(5).GeefKOBRijMetNummer(id);
            AnalyseKost4ViewModel model = new AnalyseKost4ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving3 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag3 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag3 = Convert.ToDouble(model.Bedrag3) == 0 ? null : model.Bedrag3;
            model.VolgendeLijn3 = id;
            model.ToonGroep3 = true;
            return RedirectToAction(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt3RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseKost4));
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt4RijAanpassen(int id, Analyse analyse)
        {
            KOBRij kostrij = analyse.GeefKostMetNummer(7).GeefKOBRijMetNummer(id);
            AnalyseKost4ViewModel model = new AnalyseKost4ViewModel();
            if (kostrij != null)
            {
                model.Beschrijving4 = kostrij.GeefKOBVakMetNummer(1).Data;
                model.Bedrag4 = kostrij.GeefKOBVakMetNummer(2).Data;
            }
            model.Bedrag4 = Convert.ToDouble(model.Bedrag4) == 0 ? null : model.Bedrag4;
            model.VolgendeLijn4 = id;
            model.ToonGroep4 = true;
            return RedirectToAction(nameof(AnalyseKost4), model);
        }

        [ServiceFilter(typeof(AnalyseFilter))]
        public IActionResult AnalyseKost4Punt4RijVerwijderen(int id, Analyse analyse)
        {
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
            }

            return RedirectToAction(nameof(AnalyseKost4));
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
