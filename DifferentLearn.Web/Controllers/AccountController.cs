using DifferentLearn.Core.Convertors;
using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Generator;
using DifferentLearn.Core.Security;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) 
            {
                return View(register);
            }
            if (await _userService.IsExistUserName(register.UserName)) 
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمیباشد");
                return View(register);
            }
            if (await _userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمیباشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email=FixedText.FixEmail(register.Email),
                IsActive=false,
                Password= PasswordHelper.EncodePasswordMD5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar= "Default.jpg",
                UserName=register.UserName
            };
            _userService.AddUser(user);
            return View("SuccessRegister",user);
        }
    }
}
