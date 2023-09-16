using DifferentLearn.Data.Entites.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.User
{
    public class Role
    {
        public Role()
        {
            
        }

        [Key]
        public int RoleId { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید!!!")]
        [Display(Name = "عنوان نقش")]
        [MaxLength(200,ErrorMessage ="{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string RoleTitle { get; set; }
        public bool IsDelete { get; set; }

        #region Relations
        public  ICollection<UserRole>? UserRoles { get; set; }
        public  ICollection<RolePermission>? RolePermissions { get; set; }
        #endregion

    }
}
