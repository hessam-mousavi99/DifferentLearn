using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.User
{
    public class UserRole
    {
        public UserRole()
        {
            
        }

        [Key]
        public int URId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        #region Relations
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
        #endregion
    }
}
