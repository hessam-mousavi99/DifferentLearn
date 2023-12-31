﻿using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DifferentLearn.Web.Controllers
{

    public class HomeController : Controller
    {

        private IWalletService _walletService;
        private ICourseService _courseService;
        public HomeController(IWalletService walletService, ICourseService courseService)
        {
            _walletService = walletService;
            _courseService = courseService;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetShowCourseListViewItemAsync());
        }



        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = await _walletService.GetWalletByWalletIdAsync(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    await _walletService.UpdateWalletAsync(wallet);
                }

            }
            return View();
        }

        public async Task<IActionResult> GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="انتخاب کنید",
                    Value=""
                }
            };
            list.AddRange(await _courseService.GetSubGroupFroManageCourseAsync(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/assets/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
        public IActionResult Error404()
        {
            return View();
        }
    }


}
