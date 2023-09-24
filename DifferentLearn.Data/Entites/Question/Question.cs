using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Question
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }
        [Display(Name = "عنوان پرسش")]
        [Required(ErrorMessage = "عنوان سوال را وارد کنید!")]
        [MaxLength(400)]
        public string Title { get; set; }
        [Display(Name = "متن پرسش")]
        [Required(ErrorMessage = "متن سوال را وارد کنید!")]
        public string Body { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        #region Relation
        [ForeignKey("CourseId")]
        public Course.Course? Course { get; set; }
        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        public ICollection<Answer>? Answers { get; set; }
        #endregion

    }
}
