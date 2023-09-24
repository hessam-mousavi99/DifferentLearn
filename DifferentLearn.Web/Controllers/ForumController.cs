using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Question;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DifferentLearn.Web.Controllers
{
    public class ForumController : Controller
    {
        IForumService _forumService;
        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }
        public async Task<IActionResult> Index(int? courseid,string filter="")
        {
            ViewBag.CourseId=courseid;
            return View(await _forumService.GetQuestionsAsync(courseid,filter));
        }
        [Authorize]
        public IActionResult CreateQuestion(int id)
        {
            Question question = new Question()
            {
                CourseId = id
            };
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            question.UserId = int.Parse(User.FindFirstValue(claimType: ClaimTypes.NameIdentifier).ToString());
            if (!ModelState.IsValid)
            {
                return View(question);
            }
            int questionId = await _forumService.AddQuestionAsync(question);

            return Redirect($"/Forum/ShowQuestion/{questionId}");
        }

        public async Task<IActionResult> ShowQuestion(int id)
        {
            return View(await _forumService.ShowQuestionAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Answer(int id, string Body)
        {
            if (!string.IsNullOrEmpty(Body))
            {
                var sanitizer = new HtmlSanitizer();
                Body = sanitizer.Sanitize(Body);
                await _forumService.AddAnswerAsync(new Answer()
                {
                    BodyAnswer = Body,
                    CreateDate = DateTime.Now,
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()),
                    QuestionId = id

                });
            }
            return RedirectToAction("ShowQuestion", new { id = id });
        }

        [Authorize]
        public async Task<IActionResult> SelectTrueAnswer(int questionid, int answerid)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var question = await _forumService.ShowQuestionAsync(questionid);
            if (question.Question.UserId == currentUserId)
            {
                _forumService.ChangeIsTrueAnswerAsync(questionid, answerid);
            }
            return RedirectToAction("ShowQuestion", new { id = questionid });
        }
    }
}
