using EasyCashIdentityProject.Entity.Concrete;
using EasyCashIdentityProject.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginView)
        {
            var result = await _signInManager.PasswordSignInAsync(loginView.Username, loginView.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginView.Username);
                if (user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "MyProfile");
                }
            }
            return View();
        }
    }
}
