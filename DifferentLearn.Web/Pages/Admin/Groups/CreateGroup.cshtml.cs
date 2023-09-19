using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Groups
{
    public class CreateGroupModel : PageModel
    {
        ICourseService _courseService;
        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseGroup  CourseGroup { get; set; }
        public void OnGet(int? id)
        {
            CourseGroup = new CourseGroup()
            {
                GroupTitle = "",
                ParentId = id,
            };
        }

        public async Task<IActionResult> OnPost ()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           await _courseService.AddGroupAsync(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
