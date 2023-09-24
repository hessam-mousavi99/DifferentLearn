using DifferentLearn.Core.DTOs.Question;
using DifferentLearn.Data.Entites.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IForumService
    {
        #region Question
        Task<int> AddQuestionAsync(Question question);
        Task<ShowQuestionViewModel> ShowQuestionAsync(int id);
        #endregion

        #region Answer
        Task AddAnswerAsync(Answer answer);
        Task ChangeIsTrueAnswerAsync(int qId,int aId);
        #endregion
    }
}
