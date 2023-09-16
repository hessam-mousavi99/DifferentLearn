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
        Task<Course> GetCourseByIdAsync(int courseid);

        Task UpdateCourseAsync(Course course, IFormFile imgcourse, IFormFile democourse);
        #endregion


        #region Episode
        Task<int> AddEpisodeAsync(CourseEpisode courseEpisode, IFormFile episodefile);
        bool CheckExistFile(string filename);
        Task<List<CourseEpisode>> GetListEpisodeCourseAsync(int courseid);
        Task<CourseEpisode> GetEpisodeByIdAsync(int episodeid);
        Task EditEpisodeAsync(CourseEpisode courseepisode, IFormFile episodefile);
        Task<Tuple<List<ShowCourseListViewItem>,int>> GetShowCourseListViewItemAsync(int pageId=1,string filter="",string getType="all",string orderByType="date",int startPrice=0,int EndPrice=0,List<int> selectedGroups=null, int take = 0);
        Task<CourseInfoViewModel> GetCourseInfoForShowAsync(int courseid);
        #endregion
    }
}
