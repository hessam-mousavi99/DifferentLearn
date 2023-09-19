using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Course;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        IOrderService _orderService;
        ICourseService _courseService;
        IUserService _userService;
        public CourseController(ICourseService courseService, IOrderService orderService,IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;

        }
        public async Task<IActionResult> Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date",
            int startPrice = 0, int EndPrice = 0, List<int> selectedGroups = null, int take = 9)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.PageId = pageId;
            ViewBag.Groups = await _courseService.GetAllGroupAsync();
            return View(await _courseService.GetShowCourseListViewItemAsync(pageId, filter, getType, orderByType, startPrice, EndPrice, selectedGroups, take));
        }

        [Route("ShowCourse/{id}")]
        public async Task<IActionResult> ShowCourse(int id)
        {
            var courseDetail = await _courseService.GetCourseInfoForShowAsync(id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            return View(courseDetail);
        }

        [Authorize]
        public async Task<IActionResult> BuyCourse(int id)
        {
            int orderid = await _orderService.AddOrderAsync(User.Identity.Name, id);
            return Redirect("/userpanel/order/ShowOrder/" + orderid);
        }
        [Route("DownloadFile/{episodeid}")]
        public async Task<IActionResult> DownloadFile(int episodeid)
        {
           
            CourseEpisode episode = await _courseService.GetEpisodeByIdAsync(episodeid);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/coursefiles", episode.EpisodeFileName);
            string filename = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                byte[] file=System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", filename);
            }
            if (User.Identity.IsAuthenticated)
            {
                if (await _orderService.IsUserInCourseAsync(User.Identity.Name,episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", filename);
                }
            }

            return Forbid();
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment(CourseComment comment) 
        {
            User user =await _userService.GetUserByUserNameAsync(User.Identity.Name);
            
            comment.CreateDate=DateTime.Now;
            comment.UserId = user.UserId;
            await _courseService.AddCommentAsync(comment);
          
            return View("ShowComment",await _courseService.GetCourseCommentsAsync(comment.CourseId));
        }

        public async Task<IActionResult> ShowComment(int id,int pageid=1)
        {
            return View(await _courseService.GetCourseCommentsAsync(id,pageid));
        }
    }
}
