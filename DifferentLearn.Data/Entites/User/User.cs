using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.User
{
    public class User
    {
        public User()
        {
            
        }
        [Key]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string UserName { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string Password { get; set; }
        
        [Display(Name = "کد فعالسازی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string? ActiveCode { get; set; }
        
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        
        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string? UserAvatar { get; set; }
        
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        public bool IsDelete { get; set; }

        #region Relations
        public  List<UserRole>? UserRoles { get; set; }

        public  List<Wallet.Wallet>? Wallets { get; set; }
        public  List<Course.Course>? Courses { get; set; }
        #endregion

    }
}
