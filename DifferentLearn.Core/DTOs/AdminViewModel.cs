using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.DTOs
{
    public class UsersForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class CreateUserViewModel
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

        public IFormFile UserAvatar { get; set; }
        //public string AvatarName { get; set; }
        //public List<int> SelectedRoles { get; set; }
    }
}
