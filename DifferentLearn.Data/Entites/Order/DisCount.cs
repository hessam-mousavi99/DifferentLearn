using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Order
{
    public class DisCount
    {
        [Key]
        public int DiscountId { get; set; }
        [Display(Name = "کد تخفیف")]  
        [Required(ErrorMessage ="لطفا {0} را وارد کنید!!!")]
        [MaxLength(150)]
        public string DiscountCode { get; set; }
        [Display(Name = "درصد کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        public int DisCountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDelete { get; set; }


        #region Relation

        public ICollection<UserDisCountCode>? UserDisCountCodes { get; set; }

        #endregion
    }
}
