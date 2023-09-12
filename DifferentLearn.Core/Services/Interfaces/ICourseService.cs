using DifferentLearn.Data.Entites.Course;
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
    }
}
