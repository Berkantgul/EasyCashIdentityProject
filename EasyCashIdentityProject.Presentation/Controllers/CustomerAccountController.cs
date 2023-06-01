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
        public async Task<IActionResult> IndexAsync()
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
    }
}
