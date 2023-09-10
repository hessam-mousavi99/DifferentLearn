using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("UserPanel/Wallet")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ListWallet = await _walletService.GetWalletUserAsync(User.Identity.Name);
            return View();
        }

        [Route("UserPanel/Wallet")]
        [HttpPost]
        public async Task<IActionResult> Index(ChargeWalletViewModel chargeWallet)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = await _walletService.GetWalletUserAsync(User.Identity.Name);
                return View(chargeWallet);
            }

            int walletId = await _walletService.ChargeWalletAsync(User.Identity.Name, chargeWallet.Amount, "شارژ حساب");

            #region Online Payment
            var payment = new ZarinpalSandbox.Payment(chargeWallet.Amount);
            var response = await payment.PaymentRequest("شارژ کیف پول", "https://localhost:44361/OnlinePayment/" + walletId);
            if (response.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Authority);
            }

            #endregion
            // a page that tell u response have problem 
            return null;
        }
    }
}
