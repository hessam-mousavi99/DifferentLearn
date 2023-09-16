using DifferentLearn.Data.Entites.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.DTOs.Course
{
   public class ShowCourseForAdminViewModel
    {
        //public List<DifferentLearn.Data.Entites.Course.Course> Courses { get; set; }
        public int CourseId { get; set; }
        public string  Title { get; set; }
        public string  ImageName { get; set; }
        public int EpisodeCount { get; set; }
        public int Price {  get; set; }
      
    }
    public class PagingViewModel
    {
       public List<ShowCourseForAdminViewModel> showCourseForAdminViewModels { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }

    public class ShowCourseListViewItem
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class CourseInfoViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }
        public string CourseImage { get; set; }
        public int Price { get; set; }
        public int EpisodeCount { get; set; }
        public List<CourseEpisode> Episodes { get; set; }
        public int Userscount { get; set; }
        public TimeSpan? AllEpisodeTime { get; set; }
        public string? Demofile { get; set; }
        public string Teacher { get; set; }
        public string TeacherImage { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public DateTime CreateD { get; set; }
        public DateTime? UpdateD { get; set; }
        public string? Tags { get; set; }

    }
}
