using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Order
{
    public class DisCount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }
        [Required]
        public int DisCountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
