using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class CourseGroup
    {
        public CourseGroup()
        {
            
        }
        [Key]
        public int GroupId { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string GroupTitle { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }
        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }


        #region Relations
        [ForeignKey("ParentId")]
        public ICollection<CourseGroup>? CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public ICollection<Course>? Courses { get; set; }
        [InverseProperty("SubCourseGroup")]
        public ICollection<Course>? SubGroups { get; set; }
        #endregion
    }
}
