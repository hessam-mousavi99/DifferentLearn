using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Permission
{
    public class Permission
    {

        public Permission()
        {
            
        }
        [Key]
        public int PermissionId { get; set; }
      
        [Display(Name = "عنوان نقش")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public virtual List<Permission>? Permissions { get; set; }

        public virtual List<RolePermission>? RolePermissions { get; set; }
        #endregion



    }
}
