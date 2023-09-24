using DifferentLearn.Core.DTOs.Question;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Question;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Services
{
    public class ForumService : IForumService
    {
        DiffLearnContext _context;
        public ForumService(DiffLearnContext diffLearnContext)
        {
            _context = diffLearnContext;
        }

        public async Task AddAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddQuestionAsync(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question.QuestionId;

        }

        public async Task ChangeIsTrueAnswerAsync(int qId, int aId)
        {
           var answers=_context.Answers.Where(a=>a.QuestionId==qId);
            foreach (var item in answers)
            {
                item.IsTrue = false;
                if (item.AnswerId == aId)
                {
                    item.IsTrue = true; 
                }
            }
            _context.UpdateRange(answers);
            await _context.SaveChangesAsync();
        }

        public async Task<ShowQuestionViewModel> ShowQuestionAsync(int id)
        {
            var question = new ShowQuestionViewModel();
            question.Question = await _context.Questions.Include(q => q.User).FirstOrDefaultAsync(q => q.QuestionId == id);
            question.Answers = await _context.Answers.Include(a => a.User).Where(a => a.QuestionId == id).ToListAsync();
            return question;

        }
    }
}
