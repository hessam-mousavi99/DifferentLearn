using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Order
{
    public class Order
    {
       
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public User.User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        #endregion
    }
}
