using AspNetCoreHero.ToastNotification.Abstractions;
using EasyCashIdentityProject.Entity.Concrete;
using EasyCashIdentityProject.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.Presentation.Controllers
{
    public class ConfirmEmailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;

        public ConfirmEmailController(UserManager<AppUser> userManager,INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.Email = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
            if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed= true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _notyfService.Success("Hesabınız Aktifleştirildi");
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }
    }
}
