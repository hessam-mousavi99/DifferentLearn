using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.DTOs
{
    #region Register
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "تکرار کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    #endregion

    #region Login
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    #endregion
}
