using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Aalstprojecten2_groep4DOTNET.Models.NogViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aalstprojecten2_groep4DOTNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJobCoachRepository _jobCoachRepository;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IJobCoachRepository jobCoachRepository)
        {
            _userManager = userManager;
            _jobCoachRepository = jobCoachRepository;
        }
        public IActionResult Index()
        {
            if (_jobCoachRepository.GetByEmail(User.Identity.Name).MoetWachtwoordVeranderen)
            {
                return RedirectToAction(nameof(VeranderWachtwoord));
            }
            return View();
        }

        public IActionResult VeranderWachtwoord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VeranderWachtwoord(VeranderWachtwoordViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Wachtwoord);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Wachtwoord", "Wachtwoord moet minstens 6 karakters lang zijn");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //todo wachtwoord veranderen
                    JobCoach jc = _jobCoachRepository.GetByEmail(User.Identity.Name);
                    
                    jc.Wachtwoord = model.Wachtwoord;
                    jc.MoetWachtwoordVeranderen = false;
                    _jobCoachRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
