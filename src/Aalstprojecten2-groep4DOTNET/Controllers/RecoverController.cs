using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.NogViewModels;
using Aalstprojecten2_groep4DOTNET.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class RecoverController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJobCoachRepository _jobCoachRepository;

        public RecoverController(
            UserManager<ApplicationUser> userManager, IJobCoachRepository jobCoachRepository)
        {
            _userManager = userManager;
            _jobCoachRepository = jobCoachRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WachtwoordVergetenViewModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email.Equals(model.Email));
            if (user == null)
            {
                ModelState.AddModelError("Email", "Er is geen account geregistreerd op dit emailadres");
            }
            else
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code, email = model.Email },
                    protocol: HttpContext.Request.Scheme);

                JobCoach jc = _jobCoachRepository.GetByEmail(model.Email);
                MailVerzender.VerzendMailWachtwoordVergeten(jc.Naam + " " + jc.Voornaam, model.Email, callbackUrl);

            }
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);


            
        }
    }
}
