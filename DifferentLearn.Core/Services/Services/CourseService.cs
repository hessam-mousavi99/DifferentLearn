using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Course;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.CourseGroups.Where(g=>g.ParentId==null)
                .Select(g=>new SelectListItem()
                {
                    Text=g.GroupTitle,
                    Value=g.GroupId.ToString(),
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCourseLevelAsync()
        {

            return await _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value=l.LevelId.ToString(),
                Text=l.LevelTitle
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
            return await _context.UserRoles.Where(r=>r.RoleId==2).Include(r=>r.User).Select(r=>new SelectListItem()
            {
                Value=r.UserId.ToString(),
                Text=r.User.UserName
            }).ToListAsync() ;
        }
    }
}
