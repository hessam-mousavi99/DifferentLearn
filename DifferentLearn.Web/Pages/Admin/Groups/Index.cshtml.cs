using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Groups
{
    public class IndexModel : PageModel
    {
        ICourseService _courseservice;
        public IndexModel(ICourseService courseService)
        {
                _courseservice = courseService;
        }
        public List<CourseGroup> CourseGroups { get; set; }
        public async Task OnGet()
        {
            CourseGroups=await _courseservice.GetAllGroupAsync();
        }
    }
}
