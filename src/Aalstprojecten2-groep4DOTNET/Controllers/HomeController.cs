﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Filters;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.AccountViewModels;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.ViewModels.Home;
using Aalstprojecten2_groep4DOTNET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobCoachRepository _jobCoachRepository;
        private readonly IAnalyseRepository _analyseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInterneMailJobcoachRepository _interneMailJobcoachRepository;
        private readonly IDoelgroepRepository _doelgroepRepository;
        private readonly IAdminMailRepository _adminMailRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IHostingEnvironment _environment;

        public HomeController(IJobCoachRepository jobCoachRepository, IAnalyseRepository analyseRepository, UserManager<ApplicationUser> userManager, IInterneMailJobcoachRepository interneMailJobcoachRepository, IDoelgroepRepository doelgroepRepository, IAdminMailRepository adminMailRepository, IAdminRepository adminRepository, IHostingEnvironment environment)
        {
            _jobCoachRepository = jobCoachRepository;
            _analyseRepository = analyseRepository;
            _userManager = userManager;
            _interneMailJobcoachRepository = interneMailJobcoachRepository;
            _doelgroepRepository = doelgroepRepository;
            _adminMailRepository = adminMailRepository;
            _adminRepository = adminRepository;
            _environment = environment;
        }
        public IActionResult Index()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            IEnumerable<Analyse> analyses = _analyseRepository.GetAllNietGearchiveerd(User.Identity.Name);
            Resultaat r = new Resultaat(_doelgroepRepository.GetAll());
            foreach (Analyse a in analyses)
            {
                r.BerekenResultaatVanAnalyse(a);
            }
            return View(new TeTonenAnalysesViewModel(analyses));
        }

        [HttpPost]
        public IActionResult VerstuurPdfNaarServer(TeTonenAnalysesViewModel model)
        {
            var pdfBinary = Convert.FromBase64String(model.PdfEncrypted);
            var dir = Path.Combine(_environment.WebRootPath, "pdfs");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileName = dir + "\\pdf_" + User.Identity.Name + ".pdf";

            using (var fs = new FileStream(fileName, FileMode.Create))
            using (var writer = new BinaryWriter(fs))
            {
                writer.Write(pdfBinary, 0, pdfBinary.Length);
            }
            MailMetPdfViewModel model2 = new MailMetPdfViewModel();
            model2.OntvangerMail = model.OntvangerEmail;

            return View(nameof(VerstuurMailMetPdf), model2);
        }

        [HttpPost]
        public async Task<IActionResult> VerstuurMailMetPdf(MailMetPdfViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    var dir = Path.Combine(_environment.WebRootPath, "pdfs");
                    var fileName = "";
                    if (Directory.Exists(dir))
                    {
                        fileName = dir + "\\pdf_" + User.Identity.Name + ".pdf";
                    }


                    await MailVerzender.StuurPdf(jc.Voornaam + jc.Naam, jc.Email, model.OntvangerMail, model.Onderwerp, model.Inhoud, fileName);
                    TempData["message"] = "Uw email naar " + model.OntvangerMail + " werd succesvol verstuurd.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["error"] = "Iets is misgelopen, uw email werd niet verstuurd.";
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            return View(model);
        }

        public IActionResult AnimatiesAanUit()
        {
            JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
            jc.WilAnimaties = !jc.WilAnimaties;
            _jobCoachRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            Analyse analyse = _analyseRepository.GetById(User.Identity.Name, id);
            if (analyse == null)
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProfielAanpassen()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
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
        public IActionResult ProfielAanpassenKeerTerug(ProfielAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return View("ProfielAanpassen", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfielAanpassen(ProfielAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
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
                    TempData["message"] = "Uw profiel is succesvol aangepast.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    TempData["error"] = "Iets is misgelopen, uw profiel is niet aangepast.";
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ProfielAanpassenDoorgaanZonderOpslaan(ProfielAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            if (ControleerOfModelVerandertIs(model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(WachtwoordAanpassen));
        }
        [HttpPost]
        public async Task<IActionResult> DoorgaanMetOpslaan(ProfielAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
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
                    TempData["message"] = "Uw profiel is succesvol aangepast.";
                    return RedirectToAction(nameof(WachtwoordAanpassen));
                }
                catch
                {
                    TempData["error"] = "Iets is misgelopen, uw profiel is niet aangepast.";
                }
            }
            return RedirectToAction(nameof(ProfielAanpassenDoorgaanMetOpslaanNietMogelijk), model);
        }

        public IActionResult ProfielAanpassenDoorgaanMetOpslaanNietMogelijk(ProfielAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return View(model);
        }

        public IActionResult DoorgaanZonderOpslaan()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return RedirectToAction(nameof(WachtwoordAanpassen));
        }

        public IActionResult WachtwoordAanpassen()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return View(new WachtwoordAanpassenViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> WachtwoordAanpassen(WachtwoordAanpassenViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
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

                            jc.MoetWachtwoordVeranderen = false;
                            _jobCoachRepository.SaveChanges();
                            TempData["message"] = "Uw wachtwoord is succesvol aangepast.";
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
            TempData["error"] = "Uw wachtwoord is niet aangepast.";
            return View(new WachtwoordAanpassenViewModel());
        }

        public IActionResult ContacteerAdmin()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            return View(new ContacteerAdminViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ContacteerAdmin(ContacteerAdminViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    await MailVerzender.ContacteerAdmin(jc.Naam + " " + jc.Voornaam, jc.Email, model.Onderwerp, model.Inhoud);
                    Admin admin = _adminRepository.GetByEmail("bart@kairosnu.be");
                    AdminMail mail = new AdminMail(jc, admin, model.Onderwerp, model.Inhoud, DateTime.Now);
                    _adminMailRepository.Add(mail);
                    _adminMailRepository.SaveChanges();
                    TempData["message"] = "Uw bericht werd succesvol naar de admin verstuurd.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            TempData["error"] = "Iets is misgelopen, uw bericht werd niet naar de admin verstuurd.";
            return View(model);
        }

        public IActionResult OverzichtMailbox()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
            foreach (InterneMailJobcoach m in mails)
            {
                m.Jobcoach = jc;
            }
            return View(new OverzichtMailboxViewModel(mails) {WilAnimaties = jc.WilAnimaties});
        }

        public IActionResult GeselecteerdeMail(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            try
            {
                JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                InterneMailJobcoach mail = _interneMailJobcoachRepository.GetById(User.Identity.Name, id);
                mail.IsGelezen = true;
                _interneMailJobcoachRepository.SaveChanges();
                mail.Jobcoach = jc;
                IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
                foreach (InterneMailJobcoach m in mails)
                {
                    m.Jobcoach = jc;
                }
                OverzichtMailboxViewModel model = new OverzichtMailboxViewModel(mails);
                model.GeopendeMail = new MailViewModel(mail);
                model.GeopendeMailId = mail.InterneMailId;
                model.WilAnimaties = jc.WilAnimaties;
                return View(nameof(OverzichtMailbox), model);
            }
            catch
            {
                
            }
            IEnumerable<InterneMailJobcoach> mijnMails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            JobCoach j = _jobCoachRepository.GetByEmail(User.Identity.Name);
            foreach (InterneMailJobcoach m in mijnMails)
            {
                m.Jobcoach = j;
            }
            return View(nameof(OverzichtMailbox), new OverzichtMailboxViewModel(mijnMails) {WilAnimaties = j.WilAnimaties});
        }

        public IActionResult VerwijderMail(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            InterneMailJobcoach mail = _interneMailJobcoachRepository.GetById(User.Identity.Name, id);
            if (mail == null)
                return RedirectToAction(nameof(OverzichtMailbox));
            ViewData["mail"] = mail.InterneMail.Afzender.Voornaam + " " + mail.InterneMail.Afzender.Naam + " met onderwerp " + mail.InterneMail.Onderwerp;
            return View();
        }

        [HttpPost]
        [ActionName("VerwijderMail")]
        public IActionResult BevestigVerwijderMail(int id)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            try
            {
                InterneMailJobcoach mail = _interneMailJobcoachRepository.GetById(User.Identity.Name, id);
                _interneMailJobcoachRepository.Delete(mail);
                _interneMailJobcoachRepository.SaveChanges();
                TempData["message"] = "De mail van " + mail.InterneMail.Afzender.Voornaam + " " + mail.InterneMail.Afzender.Naam + " met onderwerp " +
                                      mail.InterneMail.Onderwerp + " werd succesvol verwijderd";
                
            }
            catch
            {
                TempData["error"] = "Iets is misgelopen, de mail werd niet verwijderd.";

            }
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult MarkeerGeselecteerdeAlsGelezen()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
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
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            ICollection<InterneMailJobcoach> geselecteerdeMails = new List<InterneMailJobcoach>();
            foreach (InterneMailJobcoach m in mails)
            {
                string id = "checkbox" + m.InterneMailId;
                string check = Request.Form[id];
                if (check != null && check.Equals("on"))
                {
                    geselecteerdeMails.Add(m);
                }
            }
            HttpContext.Session.SetString("mails", JsonConvert.SerializeObject(geselecteerdeMails));
            
            return RedirectToAction(nameof(BevestigVerwijderGeselecteerde));
        }

        public IActionResult BevestigVerwijderGeselecteerde()
        {
            AnalyseFilter.HaalAnalyseUitSessie(HttpContext);
            ViewData["mails"] =
                JsonConvert.DeserializeObject<ICollection<InterneMailJobcoach>>(HttpContext.Session.GetString("mails")).Count;
            return View();
        }

        [HttpPost]
        [ActionName("BevestigVerwijderGeselecteerde")]
        public IActionResult BevestigBevestigVerwijderGeselecteerde()
        {
            AnalyseFilter.HaalAnalyseUitSessie(HttpContext);
            ICollection<InterneMailJobcoach> mails = JsonConvert.DeserializeObject<ICollection<InterneMailJobcoach>>(HttpContext.Session.GetString("mails"));
            if (mails == null)
            {
                TempData["error"] = "Er is iets misgelopen, de mails werden niet verwijderd";
                return RedirectToAction(nameof(Index));
            }
            foreach (InterneMailJobcoach m in mails)
            {
                _interneMailJobcoachRepository.Delete(_interneMailJobcoachRepository.GetById(User.Identity.Name, m.InterneMailId));
                _interneMailJobcoachRepository.SaveChanges();
            }
            TempData["message"] = "De geselecteerde mails zijn succesvol verwijderd";
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult MarkeerAlleAlsGelezen()
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            IEnumerable<InterneMailJobcoach> mails = _interneMailJobcoachRepository.GetAll(User.Identity.Name);
            if (mails == null)
            {
                TempData["error"] = "Er is iets misgelopen, de mails werden niet als gelezen gemarkeerd";
                return RedirectToAction(nameof(Index));
            }
            foreach (InterneMailJobcoach m in mails)
            {
                _interneMailJobcoachRepository.GetById(User.Identity.Name, m.InterneMailId).IsGelezen = true;
                _interneMailJobcoachRepository.SaveChanges();
            }
            TempData["message"] = "De geselecteerde mails zijn succesvol als gelezen gemarkeerd";
            return RedirectToAction(nameof(OverzichtMailbox));
        }

        [HttpPost]
        public IActionResult BeantwoordMail(OverzichtMailboxViewModel model)
        {
            AnalyseFilter.ZetSessieLeeg(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    Admin admin = _adminRepository.GetByEmail(model.Ontvanger);
                    AdminMail mail = new AdminMail(jc, admin, model.Onderwerp, model.Inhoud, DateTime.Now);
                    _adminMailRepository.Add(mail);
                    _adminMailRepository.SaveChanges();
                    TempData["message"] = "Uw antwoord werd succesvol verzonden";
                    return RedirectToAction(nameof(OverzichtMailbox));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            TempData["error"] = "Er is iets misgelopen, uw antwoord werd niet verzonden";
            return View(nameof(OverzichtMailbox));
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
