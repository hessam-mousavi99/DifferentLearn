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
}
