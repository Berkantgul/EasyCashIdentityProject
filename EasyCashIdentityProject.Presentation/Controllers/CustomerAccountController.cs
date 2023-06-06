using EasyCashIdentityProject.Dto.Dtos.AppUserDto;
using EasyCashIdentityProject.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.Presentation.Controllers
{
    public class CustomerAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomerAccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUser = new AppUserEditDto
            {
                Name = user.Name,
                Surname = user.Surname,
                District = user.District,
                City = user.City,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
        {
            if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.PhoneNumber = appUserEditDto.PhoneNumber;
                user.Surname = appUserEditDto.Surname;
                user.City = appUserEditDto.City;
                user.District = appUserEditDto.District;
                user.ImageUrl = appUserEditDto.ImageUrl;
                user.Email = appUserEditDto.Email;
                user.Name = appUserEditDto.Name;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,appUserEditDto.Password);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(appUserEditDto);
            
        }
    }
}
