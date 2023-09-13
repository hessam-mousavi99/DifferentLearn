using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.DTOs.Course;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseservice;
        public IndexModel(ICourseService courseService)
        {
            _courseservice = courseService;
        }
        public PagingViewModel pagingViewModels { get; set; }
        public async Task OnGet(int pageid = 1, string filterCourseTitle = "")
        {
            pagingViewModels = await _courseservice.PagingForCoursesForAdminAsync(pageid, filterCourseTitle);
        }
    }
}
