using DifferentLearn.Data.Entites.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.DTOs.Question
{
    public class ShowQuestionViewModel
    {
        public Data.Entites.Question.Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
