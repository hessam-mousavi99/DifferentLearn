using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class EditEpisodeModel : PageModel
    {
        ICourseService _courseService;
        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public async Task OnGet(int id)
        {
            CourseEpisode = await _courseService.GetEpisodeByIdAsync(id);
        }

        public async Task<IActionResult> OnPost(IFormFile? EpisodeUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (EpisodeUp != null)
            {
                if (_courseService.CheckExistFile(EpisodeUp.FileName))
                {
                    ViewData["IsExistFile"] = true;
                    return Page();
                }
            }

            await _courseService.EditEpisodeAsync(CourseEpisode, EpisodeUp);

            return Redirect("/admin/courses/indexepisode/" + CourseEpisode.CourseId);
        }
    }
}
