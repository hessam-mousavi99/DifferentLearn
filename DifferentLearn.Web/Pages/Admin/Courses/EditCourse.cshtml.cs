using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DifferentLearn.Web.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Course Course { get; set; }
        public async Task OnGet(int id)
        {
            Course = await _courseService.GetCourseByIdAsync(id);


            var group = await _courseService.GetGroupFroManageCourseAsync();
            ViewData["Groups"] = new SelectList(group, "Value", "Text", Course.GroupId);

            var subGroups = await _courseService.GetSubGroupFroManageCourseAsync(Convert.ToInt32(group.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Course.SubGroupId ?? 0);

            var teachers = await _courseService.GetTeachersAsync();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", Course.TeacherId);

            var levels = await _courseService.GetCourseLevelAsync();
            ViewData["CourseLevel"] = new SelectList(levels, "Value", "Text", Course.LevelId);

            var statuses = await _courseService.GetCourseStatusAsync();
            @ViewData["CourseStatus"] = new SelectList(statuses, "Value", "Text", Course.StatusId);
        }

        public async Task<IActionResult> OnPost(IFormFile ImgCourseUp, IFormFile? DemoUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _courseService.UpdateCourseAsync(Course, ImgCourseUp, DemoUp);
            return RedirectToPage("Index");
        }
    }
}

