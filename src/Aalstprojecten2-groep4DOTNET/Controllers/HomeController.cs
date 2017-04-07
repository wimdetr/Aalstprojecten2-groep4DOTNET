using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.AccountViewModels;
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
            IEnumerable<Analyse> analyses = _analyseRepository.GetAllNietGearchiveerd(User.Identity.Name);
            Resultaat r = new Resultaat();
            foreach (Analyse a in analyses)
            {
                r.BerekenResultaatVanAnalyse(a);
            }
            return View(new TeTonenAnalysesViewModel(analyses));
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
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProfielAanpassen()
        {
            JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
            ProfielAanpassenViewModel origineelModel = new ProfielAanpassenViewModel()
            {
                Bus = jc.BusBedrijf,
                Email = jc.Email,
                Gemeente = jc.GemeenteBedrijf,
                Naam = jc.Naam,
                NaamBedrijf = jc.NaamBedrijf,
                Nummer = jc.NummerBedrijf,
                Postcode = jc.PostcodeBedrijf,
                Straat = jc.StraatBedrijf,
                Voornaam = jc.Voornaam
            };
            ProfielAanpassenViewModel model = new ProfielAanpassenViewModel(origineelModel);

            return View(model);
        }

        [HttpPost]
        public IActionResult ProfielAanpassen(ProfielAanpassenViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    jc.BusBedrijf = model.Bus;
                    jc.GemeenteBedrijf = model.Gemeente;
                    jc.Naam = model.Naam;
                    jc.NaamBedrijf = model.NaamBedrijf;
                    jc.NummerBedrijf = model.Nummer;
                    jc.PostcodeBedrijf = model.Postcode;
                    jc.StraatBedrijf = model.Straat;
                    jc.Voornaam = model.Voornaam;
                    _jobCoachRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        public IActionResult ProfielAanpassenDoorgaanZonderOpslaan(ProfielAanpassenViewModel model)
        {
            if (ControleerOfModelVerandertIs(model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(WachtwoordAanpassen));
        }

        public IActionResult DoorgaanMetOpslaan(ProfielAanpassenViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    jc.BusBedrijf = model.Bus;
                    jc.GemeenteBedrijf = model.Gemeente;
                    jc.Naam = model.Naam;
                    jc.NaamBedrijf = model.NaamBedrijf;
                    jc.NummerBedrijf = model.Nummer;
                    jc.PostcodeBedrijf = model.Postcode;
                    jc.StraatBedrijf = model.Straat;
                    jc.Voornaam = model.Voornaam;
                    _jobCoachRepository.SaveChanges();
                    return RedirectToAction(nameof(WachtwoordAanpassen));
                }
                catch (Exception e)
                {
                    
                }
            }
            return RedirectToAction(nameof(ProfielAanpassenDoorgaanMetOpslaanNietMogelijk), model);
        }

        public IActionResult ProfielAanpassenDoorgaanMetOpslaanNietMogelijk(ProfielAanpassenViewModel model)
        {
            return View(model);
        }

        public IActionResult DoorgaanZonderOpslaan(ProfielAanpassenViewModel model)
        {
            return RedirectToAction(nameof(WachtwoordAanpassen));
        }

        public IActionResult NietDoorgaan(ProfielAanpassenViewModel model)
        {
            return RedirectToAction(nameof(ProfielAanpassen), model);
        }

        public IActionResult WachtwoordAanpassen()
        {
            return View(new WachtwoordAanpassenViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> WachtwoordAanpassen(WachtwoordAanpassenViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    bool check = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                    if (!check)
                    {
                        ModelState.AddModelError("OldPassword", "Incorrect wachtwoord");
                    }
                    else
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                        if (result.Succeeded)
                        {
                            JobCoach jc = _jobCoachRepository.GetByEmail(user.Email);

                            jc.Wachtwoord = model.Password;
                            jc.MoetWachtwoordVeranderen = false;
                            _jobCoachRepository.SaveChanges();
                            return RedirectToAction(nameof(ProfielAanpassen));
                        }
                        ModelState.AddModelError("", "Onze excuses, er is iets verkeerd gelopen.");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(new WachtwoordAanpassenViewModel());
        }

        private bool ControleerOfModelVerandertIs(ProfielAanpassenViewModel model)
        {
            return !model.Naam.Equals(model.Origineel.Naam) 
                || !model.Voornaam.Equals(model.Origineel.Voornaam) 
                || !model.NaamBedrijf.Equals(model.Origineel.NaamBedrijf)
                || !model.Straat.Equals(model.Origineel.Straat)
                || !model.Nummer.Equals(model.Origineel.Nummer)
                || !model.Bus.Equals(model.Origineel.Bus)
                || !model.Postcode.Equals(model.Origineel.Postcode)
                || !model.Gemeente.Equals(model.Origineel.Gemeente);
        }
    }
}
