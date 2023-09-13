using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class IndexEpisodeModel : PageModel
    {
        ICourseService _courseService;
        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public async Task OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = await _courseService.GetListEpisodeCourseAsync(id);
        }
    }
}
