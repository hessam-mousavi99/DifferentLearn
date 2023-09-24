using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Question
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string BodyAnswer { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsTrue { get; set; } = false;

        #region
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
        [ForeignKey("UserId")]
        public User.User? User{ get; set; }
        #endregion
    }
}
