using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Course Course { get; set; }
        public async Task OnGet()
        {
            var group= await _courseService.GetGroupFroManageCourseAsync();
            ViewData["Groups"] = new SelectList(group,"Value","Text");
            
            var subGroups= await _courseService.GetSubGroupFroManageCourseAsync(Convert.ToInt32(group.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var teachers = await _courseService.GetTeachersAsync();
            ViewData["Teachers"]= new SelectList(teachers, "Value", "Text");

            var levels=await _courseService.GetCourseLevelAsync();
             ViewData["CourseLevel"]=new SelectList(levels, "Value", "Text");

            var statuses=await _courseService.GetCourseStatusAsync();
            @ViewData["CourseStatus"] = new SelectList(statuses, "Value", "Text");
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return Page();
        }
    }
}
