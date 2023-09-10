using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _userService.GetUserInformationAsync(User.Identity.Name));
        }
        [Route("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            return View(await _userService.GetDataForEditProfileUserAsync(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(editProfile);
            }
            await _userService.EditProfileAsync(User.Identity.Name, editProfile);

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/login?EditProfile=true");
        }

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            string Currentusername=User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }

            if (! await _userService.CompareOldPasswordAsync(Currentusername, changePassword.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نیست");
                return View(changePassword);
            }
            await _userService.ChangeUserPasswordAsync(Currentusername, changePassword.Password);
            ViewBag.IsSuccess=true;
            return View();
        }

       

    }
}
