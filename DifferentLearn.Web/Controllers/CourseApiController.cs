using DifferentLearn.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DifferentLearn.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        DiffLearnContext _context;
        public CourseApiController(DiffLearnContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle=await _context.Courses.Where(c=>c.CourseTitle.Contains(term)).Select(c=>c.CourseTitle).ToListAsync();  
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
