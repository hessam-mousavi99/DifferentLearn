using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Components
{
    public class CourseGroupForNavComponent: ViewComponent
    {
        ICourseService _courseService;
        public CourseGroupForNavComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult((IViewComponentResult)View("CourseGroupForNav", await _courseService.GetAllGroupAsync()));

    }
}
