using DifferentLearn.Data.Entites.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class Course
    {
        public Course()
        {
            
        }
        [Key]
        public int CourseId { get; set; }
        [Required]
        public int GroupId { get; set; }

        public int? SubGroupId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "عنوان دوره")]
        [MaxLength(450, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public required string CourseTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "شرح دوره")]
        public required string CourseDescription { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "قیمت دوره")]
        public int CoursePrice { get; set; }

        [MaxLength(600)]
        public string? Tags { get; set; }

        [MaxLength(50)]
        public string? CourseImageName { get; set; }

        [MaxLength(100)]
        public string? DemoFileName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        
        public bool IsDelete { get; set; }




        #region Relation
        [ForeignKey("TeacherId")]
        public User.User? User { get; set; }
        [ForeignKey("GroupId")]
        public CourseGroup? CourseGroup { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup? SubCourseGroup { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus? CourseStatus { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel? CourseLevel { get; set; }
        public ICollection<CourseEpisode>? CourseEpisodes { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<UserCourse>? UserCourses { get; set; }
        #endregion

    }
}
