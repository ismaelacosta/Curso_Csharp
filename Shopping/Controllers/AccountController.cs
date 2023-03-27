using Microsoft.AspNetCore.Mvc;
using Shopping.Helpers;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper) { 
            _userHelper = userHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            if(User.Identity.IsAuthenticated)
            {                           // Metodo controlador
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }


            ModelState.AddModelError(string.Empty, "Email o contrasena incorrectos.");
            return View(model);
        }

        // No tiene vista
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
