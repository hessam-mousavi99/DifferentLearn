using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class UserCourse
    {
        [Key]
        public int UCId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relation

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [ForeignKey("UserId")]
        public User.User? User { get; set; }
        #endregion
    }
}
