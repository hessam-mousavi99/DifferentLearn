using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Controllers
{
    
    public class HomeController : Controller
    {

        private IWalletService _walletService;
        public HomeController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public IActionResult Index() => View();



        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"]!="" &&
                HttpContext.Request.Query["Status"].ToString().ToLower()=="ok" &&
                HttpContext.Request.Query["Authority"]!="") 
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = await _walletService.GetWalletByWalletIdAsync(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status==100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay=true;
                    await _walletService.UpdateWalletAsync(wallet);
                }

            }
            return View();
        }
    }
    
}
