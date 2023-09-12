using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Course
{
    public class CourseEpisode
    {
        public CourseEpisode()
        {
            
        }

        [Key]
        public int EpisodeId { get; set; }
        [Required]
        public int CourseId { get; set; }

        [MaxLength(400, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        public required string EpisodeTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید!!!")]
        [Display(Name = "زمان")]
        public TimeSpan EpisodeTime { get; set; }
       
        [Display(Name = "فایل")]
        public string? EpisodeFileName { get; set; }
        
        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
        public bool IsDelete { get; set; }

        #region Relation

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
        #endregion
    }
}
