using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = new CourseEpisode() { EpisodeTitle=""};
            CourseEpisode.CourseId = id;
        }

        public  async Task<IActionResult> OnPost(IFormFile EpisodeUp)
        {

            if(!ModelState.IsValid && EpisodeUp == null)
            {
                return Page();
            }
            if (_courseService.CheckExistFile(EpisodeUp.FileName))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }
            await _courseService.AddEpisodeAsync(CourseEpisode, EpisodeUp);
            return Redirect("/admin/courses/indexepisode/" + CourseEpisode.CourseId);
        }
    }
}
