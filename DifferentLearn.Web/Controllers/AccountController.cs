using DifferentLearn.Core.Convertors;
using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Generator;
using DifferentLearn.Core.Security;
using DifferentLearn.Core.Senders;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DifferentLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;
        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }
        #region Register
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
            if (await _userService.IsExistUserNameAsync(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمیباشد");
                return View(register);
            }
            if (await _userService.IsExistEmailAsync(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمیباشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMD5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Default.jpg",
                UserName = register.UserName
            };
            await _userService.AddUserAsync(user);

            string Body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", Body);
            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("login")]
        public IActionResult Login(bool EditProfile=false)
        {
            ViewBag.EditProfile=EditProfile;
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userService.LoginUserAsync(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var cliams = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(cliams, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمیباشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد.");
            return View(login);
        }

        #endregion

        #region Active Account

        public async Task<IActionResult> ActiveAccount(string id)
        {
            ViewBag.IsActive = await _userService.ActiveAccountAsync(id);
            return View();
        }
        #endregion

        #region Logout
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {

            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordviewModel forgotpassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotpassword);
            }

            string fixedEmail=FixedText.FixEmail(forgotpassword.Email);
            User user=await _userService.GetUserByEmailAsync(fixedEmail);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgotpassword);
            }
            string bodyemail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی حساب کاربری", bodyemail);
            ViewBag.IsSuccess=true;
            return View();
        }
        #endregion

        #region ResetPassword
        public ActionResult ResetPassword(string id)
        {

            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {

            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }

            User user = await _userService.GetUserByActiveCodeAsync(resetPassword.ActiveCode);

            if (user == null)
            {
                return NotFound();
            }
            string hashpass = PasswordHelper.EncodePasswordMD5(resetPassword.Password);
            user.Password = hashpass;
            await _userService.UpdateUserAsync(user);

            return Redirect("/Login");
        }
        #endregion
    }
}
