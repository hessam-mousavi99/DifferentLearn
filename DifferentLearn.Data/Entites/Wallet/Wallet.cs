using DifferentLearn.Data.Entites.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }
        
        [Key]
        public int WalletId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "نوع تراکنش")]
        public int TypeId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "مبلغ")]
        public int Amount { get; set; }
        
        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string? Description { get; set; }
        
        [Display(Name = "تایید")]
        public bool IsPay { get; set; }
        
        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public  User.User? User { get; set; }
        [ForeignKey("TypeId")]
        public  WalletType? WalletType { get; set; }
        #endregion

    }
}
