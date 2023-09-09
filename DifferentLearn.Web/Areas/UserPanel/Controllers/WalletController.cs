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
    }
}
