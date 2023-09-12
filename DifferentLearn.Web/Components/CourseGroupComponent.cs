using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Components
{
    public class CourseGroupComponent:ViewComponent
    {
        ICourseService _courseService;
        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult((IViewComponentResult)View("CourseGroup",await _courseService.GetAllGroup()));
    }
}
