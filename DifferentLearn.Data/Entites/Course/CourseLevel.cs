using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class CourseLevel
    {
        public CourseLevel()
        {
            
        }
        [Key]
        public int LevelId { get; set; }
        [Display(Name = "عنوان سطح")]
        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        public required string LevelTitle { get; set; }


        #region Relation
        public ICollection<Course>? Courses { get; set; }
        #endregion
    }
}
