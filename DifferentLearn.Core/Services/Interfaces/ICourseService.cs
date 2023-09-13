using DifferentLearn.Core.DTOs.Course;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group
        Task<List<CourseGroup>> GetAllGroupAsync();
        Task<List<SelectListItem>> GetGroupFroManageCourseAsync();
        Task<List<SelectListItem>> GetSubGroupFroManageCourseAsync(int groupid);
        Task<List<SelectListItem>> GetTeachersAsync();
        Task<List<SelectListItem>> GetCourseLevelAsync();
        Task<List<SelectListItem>> GetCourseStatusAsync();
        #endregion

        #region Course
        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdminAsync();
        Task<PagingViewModel> PagingForCoursesForAdminAsync(int pageid = 1, string filterCourseTitle = "");
        Task<int> AddCourseAsync(Course course, IFormFile imgcourse, IFormFile democourse);
        #endregion
    }
}
