using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.AccountViewModels;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Aalstprojecten2_groep4DOTNET.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobCoachRepository _jobCoachRepository;
        private readonly IAnalyseRepository _analyseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInterneMailJobcoachRepository _interneMailJobcoachRepository;


        public HomeController(IJobCoachRepository jobCoachRepository, IAnalyseRepository analyseRepository, UserManager<ApplicationUser> userManager, IInterneMailJobcoachRepository interneMailJobcoachRepository)
        {
            _jobCoachRepository = jobCoachRepository;
            _analyseRepository = analyseRepository;
            _userManager = userManager;
            _interneMailJobcoachRepository = interneMailJobcoachRepository;
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
            ProfielAanpassenViewModel model = new ProfielAanpassenViewModel()
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
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfielAanpassen(ProfielAanpassenViewModel model)
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
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    user.Naam = model.Naam;
                    user.Voornaam = model.Voornaam;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ProfielAanpassenDoorgaanZonderOpslaan(ProfielAanpassenViewModel model)
        {
            if (ControleerOfModelVerandertIs(model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(WachtwoordAanpassen));
        }
        [HttpPost]
        public async Task<IActionResult> DoorgaanMetOpslaan(ProfielAanpassenViewModel model)
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
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    user.Naam = model.Naam;
                    user.Voornaam = model.Voornaam;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(WachtwoordAanpassen));
                }
                catch
                {
                    
                }
            }
            return RedirectToAction(nameof(ProfielAanpassenDoorgaanMetOpslaanNietMogelijk), model);
        }

        public IActionResult ProfielAanpassenDoorgaanMetOpslaanNietMogelijk(ProfielAanpassenViewModel model)
        {
            return View(model);
        }

        public IActionResult DoorgaanZonderOpslaan()
        {
            return RedirectToAction(nameof(WachtwoordAanpassen));
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

        public IActionResult ContacteerAdmin()
        {
            return View(new ContacteerAdminViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ContacteerAdmin(ContacteerAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    await MailVerzender.ContacteerAdmin(jc.Naam + " " + jc.Voornaam, jc.Email, model.Onderwerp, model.Inhoud);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        public IActionResult OverzichtMailbox()
        {
            return View(new OverzichtMailboxViewModel(_interneMailJobcoachRepository.GetAll(User.Identity.Name)));
        }

        public IActionResult GeselecteerdeMail(int id)
        {
            InterneMailJobcoach mail = _interneMailJobcoachRepository.GetById(User.Identity.Name, id);
            mail.IsGelezen = true;
            _interneMailJobcoachRepository.SaveChanges();
            MailViewModel model = new MailViewModel(mail);
            return View(model);
        }

        public IActionResult VerwijderMail(int id)
        {
            _interneMailJobcoachRepository.Delete(_interneMailJobcoachRepository.GetById(User.Identity.Name, id));
            _interneMailJobcoachRepository.SaveChanges();
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult MarkeerGeselecteerdeAlsGelezen()
        {
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            foreach (InterneMailJobcoach m in mails)
            {
                string id = "checkbox" + m.InterneMailId;
                string check = Request.Form[id];
                if (check != null && check.Equals("on"))
                {
                    _interneMailJobcoachRepository.GetById(User.Identity.Name, m.InterneMailId).IsGelezen = true;
                    _interneMailJobcoachRepository.SaveChanges();

                }
            }
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult VerwijderGeselecteerde()
        {
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            foreach (InterneMailJobcoach m in mails)
            {
                string id = "checkbox" + m.InterneMailId;
                string check = Request.Form[id];
                if (check != null && check.Equals("on"))
                {
                    _interneMailJobcoachRepository.Delete(m);
                    _interneMailJobcoachRepository.SaveChanges();

                }
            }


            _interneMailJobcoachRepository.SaveChanges();
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult MarkeerAlleAlsGelezen()
        {
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            foreach (InterneMailJobcoach m in mails)
            {
                _interneMailJobcoachRepository.GetById(User.Identity.Name, m.InterneMailId).IsGelezen = true;
                _interneMailJobcoachRepository.SaveChanges();
            }
            
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        private bool ControleerOfModelVerandertIs(ProfielAanpassenViewModel model)
        {
            JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
            bool isVerandert = (model.Bus == null && jc.BusBedrijf != null) || (model.Bus != null && jc.BusBedrijf == null);

            if (model.Naam == null)
            {
                isVerandert = true;
            }
            else if (!model.Naam.Equals(jc.Naam))
            {
                isVerandert = true;
            }
            
            if (model.Voornaam == null)
            {
                isVerandert = true;
            }
            else if (!model.Voornaam.Equals(jc.Voornaam))
            {
                isVerandert = true;
            }
            if (model.NaamBedrijf == null)
            {
                isVerandert = true;
            }
            else if (!model.NaamBedrijf.Equals(jc.NaamBedrijf))
            {
                isVerandert = true;
            }
            if (model.Straat == null)
            {
                isVerandert = true;
            }
            else if (!model.Straat.Equals(jc.StraatBedrijf))
            {
                isVerandert = true;
            }
            if (model.Nummer != jc.NummerBedrijf)
            {
                isVerandert = true;
            }
            if (model.Postcode != jc.PostcodeBedrijf)
            {
                isVerandert = true;
            }
            if (model.Gemeente == null)
            {
                isVerandert = true;
            }
            else if (!model.Gemeente.Equals(jc.GemeenteBedrijf))
            {
                isVerandert = true;
            }
            if (model.Bus != null && jc.BusBedrijf != null)
            {
                if (!model.Bus.Equals(jc.BusBedrijf))
                {
                    isVerandert = true;
                }
            }
            return isVerandert;
        }
    }
}
