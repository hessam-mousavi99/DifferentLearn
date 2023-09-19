using Azure;
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
using System.Net.NetworkInformation;
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
            return await _context.CourseGroups.Include(c=>c.CourseGroups).ToListAsync();
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
                string thumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/thumb", course.CourseImageName);

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
            paging.showCourseForAdminViewModels = courses.OrderBy(u => u.CourseId).Skip(skip).Take(take).ToList();
            return paging;
        }

        public async Task<Course> GetCourseByIdAsync(int courseid)
        {
            return await _context.Courses.FindAsync(courseid);
        }

        public async Task UpdateCourseAsync(Course course, IFormFile imgcourse, IFormFile democourse)
        {
            course.UpdateDate = DateTime.Now;

            if (imgcourse != null && imgcourse.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/image", course.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }
                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/thumb", course.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgcourse.FileName);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/image", course.CourseImageName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    imgcourse.CopyTo(stream);
                }

                ImageConvertor imageResize = new ImageConvertor();
                string thumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/thumb", course.CourseImageName);

                imageResize.Image_resize(imgPath, thumPath, 150);
            }


            if (democourse != null)
            {
                if (course.DemoFileName != null)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/demoes", course.DemoFileName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(democourse.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/courseavatar/demoes", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    democourse.CopyTo(stream);
                }
            }


            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddEpisodeAsync(CourseEpisode courseEpisode, IFormFile episodefile)
        {
            courseEpisode.EpisodeFileName = episodefile.FileName;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles", courseEpisode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                episodefile.CopyTo(stream);
            }

            await _context.CourseEpisodes.AddAsync(courseEpisode);
            await _context.SaveChangesAsync();
            return courseEpisode.EpisodeId;
        }

        public bool CheckExistFile(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles", filename);
            return File.Exists(filePath);

        }

        public async Task<List<CourseEpisode>> GetListEpisodeCourseAsync(int courseid)
        {
            var episodes = await _context.CourseEpisodes.Where(e => e.CourseId == courseid).ToListAsync();
            return episodes;

        }

        public async Task<CourseEpisode> GetEpisodeByIdAsync(int episodeid)
        {
            return await _context.CourseEpisodes.FindAsync(episodeid);
        }

        public async Task EditEpisodeAsync(CourseEpisode courseepisode, IFormFile episodefile)
        {

            if (episodefile != null)
            {
                string deletefilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles", courseepisode.EpisodeFileName);
                File.Delete(deletefilePath);

                courseepisode.EpisodeFileName = episodefile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles", courseepisode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodefile.CopyTo(stream);
                }
            }
            _context.CourseEpisodes.Update(courseepisode);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<ShowCourseListViewItem>, int>> GetShowCourseListViewItemAsync(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int EndPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
            {
                take = 6;
            }

            IQueryable<Course> result = _context.Courses;
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }
            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }
            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }
            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }
            if (EndPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < EndPrice);
            }
            if (selectedGroups != null && selectedGroups.Any())
            {
                //impelement
                foreach (int GroupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == GroupId || c.SubGroupId == GroupId);
                }
            }
            int skip = (pageId - 1) * take;

            int pagecount = (int)Math.Ceiling((decimal)(result.Select(c => new ShowCourseListViewItem()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                StartDate = c.CreateDate
            }).Count()) / (decimal)take);

            var query = await result.Select(c => new ShowCourseListViewItem()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                StartDate = c.CreateDate
            }).Skip(skip).Take(take).ToListAsync();

            return Tuple.Create(query, pagecount);
        }

        public async Task<CourseInfoViewModel> GetCourseInfoForShowAsync(int courseid)
        {
            Course course = await GetCourseByIdAsync(courseid);
            List<CourseEpisode> episodes = await _context.CourseEpisodes.Where(e => e.CourseId == course.CourseId).ToListAsync();
            CourseStatus status = await _context.CourseStatuses.SingleAsync(s => s.StatusId == course.StatusId);
            CourseLevel level = await _context.CourseLevels.SingleAsync(l => l.LevelId == course.LevelId);
            User teacher = await _context.Users.SingleAsync(u => u.UserId == course.TeacherId);
            List<UserCourse> userCourse = _context.UserCourses.Where(c => c.CourseId == course.CourseId).ToList();
            CourseInfoViewModel info = new CourseInfoViewModel()
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                CourseImage = course.CourseImageName,
                AllEpisodeTime = new TimeSpan(episodes.Sum(e => e.EpisodeTime.Ticks)),
                CreateD = course.CreateDate,
                UpdateD = course.UpdateDate,
                Demofile = course.DemoFileName,
                EpisodeCount = episodes.Count(),
                Description = course.CourseDescription,
                Episodes = episodes.ToList(),
                Level = level.LevelTitle,
                Price = course.CoursePrice,
                Status = status.StatusTitle,
                Tags = course.Tags,
                Teacher = teacher.UserName,
                TeacherImage = teacher.UserAvatar,
                Userscount = userCourse.Count,
            };
            return info;
        }

        public async Task AddCommentAsync(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<CourseComment>, int>> GetCourseCommentsAsync(int courseid, int pageid = 1)
        {
            int take = 5;
            int skip = (pageid - 1) * take;
            int pageCount= (int)Math.Ceiling((decimal)_context.CourseComments.Where(c=>!c.IsDelete&&c.CourseId==courseid).Count() / (decimal)take);
            return Tuple.Create(await _context.CourseComments.Include(c=>c.User).Where(c => !c.IsDelete 
            && c.CourseId == courseid).Skip(skip).Take(take).OrderByDescending(c=>c.CreateDate).ToListAsync(), pageCount);
        }

        public async Task AddGroupAsync(CourseGroup group)
        {
            _context.CourseGroups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupAsync(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseGroup> GetGroupByIdAsync(int groupid)
        {
            return await _context.CourseGroups.FindAsync(groupid);
        }
    }
}
