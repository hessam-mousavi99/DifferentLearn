using DifferentLearn.Core.Convertors;
using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.DTOs.Course;
using DifferentLearn.Core.Generator;
using DifferentLearn.Core.Security;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Course;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Services
{
    public class CourseService : ICourseService
    {
        DiffLearnContext _context;
        public CourseService(DiffLearnContext context)
        {
            _context = context;
        }
        public async Task<List<CourseGroup>> GetAllGroupAsync()
        {
            return await _context.CourseGroups.ToListAsync();
        }

        public async Task<List<SelectListItem>> GetGroupFroManageCourseAsync()
        {
            return await _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString(),
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCourseLevelAsync()
        {

            return await _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCourseStatusAsync()
        {
            return await _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSubGroupFroManageCourseAsync(int groupid)
        {
            return await _context.CourseGroups.Where(g => g.ParentId == groupid)
              .Select(g => new SelectListItem()
              {
                  Text = g.GroupTitle,
                  Value = g.GroupId.ToString(),
              }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetTeachersAsync()
        {
            return await _context.UserRoles.Where(r => r.RoleId == 2).Include(r => r.User).Select(r => new SelectListItem()
            {
                Value = r.UserId.ToString(),
                Text = r.User.UserName
            }).ToListAsync();
        }

        public async Task<int> AddCourseAsync(Course course, IFormFile imgcourse, IFormFile democourse)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";

            if (imgcourse != null && imgcourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgcourse.FileName);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/image", course.CourseImageName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    imgcourse.CopyTo(stream);
                }

                ImageConvertor imageResize = new ImageConvertor();
                string thumPath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/thumb", course.CourseImageName);

                imageResize.Image_resize(imgPath, thumPath, 150);
            }


            if (democourse != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(democourse.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/demoes", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    democourse.CopyTo(stream);
                }
            }

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdminAsync()
        {

            return await _context.Courses.Select(c => new ShowCourseForAdminViewModel
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count,
                Price = c.CoursePrice,


            }).ToListAsync();
        }
        public async Task<PagingViewModel> PagingForCoursesForAdminAsync(int pageid = 1, string filterCourseTitle = "")
        {
            List<ShowCourseForAdminViewModel> courses = await GetCoursesForAdminAsync();

            int take = 5;
            int skip = (pageid - 1) * take;
            if (!string.IsNullOrEmpty(filterCourseTitle))
            {
                courses = courses.Where(u => u.Title.Contains(filterCourseTitle)).ToList();
            }
            PagingViewModel paging = new PagingViewModel();
            paging.CurrentPage = pageid;
            paging.PageCount = (int)Math.Ceiling((decimal)courses.Count() / (decimal)take);
            paging.showCourseForAdminViewModels =  courses.OrderBy(u => u.CourseId).Skip(skip).Take(take).ToList();
            return paging;
        }
    }
}
