using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Groups
{
    public class EditGroupModel : PageModel
    {
        ICourseService _courseService;
        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseGroup CourseGroup { get; set; }
        public async Task OnGet(int id)
        {
            CourseGroup =await _courseService.GetGroupByIdAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _courseService.UpdateGroupAsync(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
