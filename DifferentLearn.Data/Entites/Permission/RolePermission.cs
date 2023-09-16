using DifferentLearn.Data.Entites.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Permission
{
    public class RolePermission
    {
        public RolePermission()
        {

        }
        [Key]
        public int RPId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }


        #region Relations

        [ForeignKey("RoleId")]
        public  Role? Role { get; set; }

        [ForeignKey("PermissionId")]
        public  Permission? Permission { get; set; }
        #endregion
    }
}
