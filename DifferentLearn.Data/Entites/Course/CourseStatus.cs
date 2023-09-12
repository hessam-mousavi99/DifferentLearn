using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class CourseStatus
    {
        public CourseStatus()
        {
            
        }
        [Key]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "عنوان وضعیت")]
        [MaxLength(150, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string StatusTitle { get; set; }


        #region Relation
        public  List<Course>? Courses { get; set; }
        #endregion

    }
}
