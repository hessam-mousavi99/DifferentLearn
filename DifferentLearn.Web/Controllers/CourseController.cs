﻿using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date",
            int startPrice = 0, int EndPrice = 0, List<int> selectedGroups = null,int take=1)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.PageId=pageId;
            ViewBag.Groups=await _courseService.GetAllGroupAsync();
            return View(await _courseService.GetShowCourseListViewItemAsync(pageId,filter,getType,orderByType,startPrice,EndPrice,selectedGroups,take));
        }
    }
}