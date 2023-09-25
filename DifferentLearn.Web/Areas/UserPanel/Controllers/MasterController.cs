using DifferentLearn.Core.DTOs.Course;
using DifferentLearn.Core.Security;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DifferentLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    [PermissionChecker(10)]
    public class MasterController : Controller
    {
        ICourseService _courseService;
        IUserService _userService;
        public MasterController(ICourseService courseService ,IUserService userService)
        {
            _courseService = courseService;
        }
        [HttpGet("master-courses")]
        public async Task<IActionResult> MasterCoursesList()
        {
            var courses = await _courseService.GetAllMasterCoursesAsync(User.Identity.Name);
            return View(courses);
        }

        [HttpGet("course-episodes/{courseid}")]
        public async Task<IActionResult> EpisodesList(int courseid)
        {
            var course=await _courseService.GetCourseByIdAsync(courseid);
            ViewBag.ID = course.CourseId;
            if (course==null)
            {
                return NotFound();
            }

            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            if (course.TeacherId!=userid)
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }
            var episodes=await _courseService.GetCourseEpisodesByCourseIdAsync(courseid);
            return View(episodes);
        }
        [HttpGet("Add-Episode/{courseid}")]
        public async Task<IActionResult>  AddEpisode(int courseid)
        {
            var course=await _courseService.GetCourseByIdAsync(courseid);
            if (course == null)
            {
                return NotFound();
            }

            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            if (course.TeacherId != userid)
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }
            var res = new AddEpisodeViewModel()
            {
                IsFree = true,
                CourseId = course.CourseId,
            };
            return View(res);
        }
        [HttpPost("Add-Episode/{courseid}")]
        public async Task<IActionResult> AddEpisode(AddEpisodeViewModel addEpisode)
        {
            if (!ModelState.IsValid)
            {
                return View(addEpisode);
            }

            if (string.IsNullOrEmpty(addEpisode.EpisodeFileName))
            {
                return View(addEpisode);
            }

            var result =await _courseService.AddEpisodeAsync(addEpisode, User.Identity.Name);

            if (result)
            {
                return RedirectToAction("EpisodesList", "Master", new { courseId = addEpisode.CourseId });
            }

            return View(addEpisode);
        }
        public IActionResult DropzoneTarget(List<IFormFile> files,int courseid)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var finalPath = path + fileName;

                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new JsonResult(new { data = fileName, status = "Success" });
                }
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}
