using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Order
{
    public class UserDisCountCode
    {
        [Key]
        public int UDId { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }



        #region Relation
        [ForeignKey("UserId")]
        public User.User? User { get; set; }
        [ForeignKey("DiscountId")]
        public DisCount? DisCount { get; set; }

        #endregion
    }
}
