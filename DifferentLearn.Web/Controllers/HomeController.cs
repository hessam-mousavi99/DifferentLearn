﻿using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Sallam dostan!!!!");
        }
    }
}
