using Examen_MVC.Models;
using Examen_MVC.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Interimkantoor.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;

        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            CustomUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Emailadres is nog niet bevestigd.");
                return View(model);
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                // Nooit exacte informatie geven: zeg alleen dat combinatie vekeerd is...
                ModelState.AddModelError("", "Verkeerde logincombinatie!");
                return View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (result.IsLockedOut)
                ModelState.AddModelError("", "Account geblokkeerd!!");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Coffee");
            }

            ModelState.AddModelError("", "Ongeldige loginpoging");
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Coffee");
        }
    }
}