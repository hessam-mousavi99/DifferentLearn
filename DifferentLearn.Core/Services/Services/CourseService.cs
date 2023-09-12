using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Course;
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
        public async Task<List<CourseGroup>> GetAllGroup()
        {
           return await _context.CourseGroups.ToListAsync();
        }
    }
}
