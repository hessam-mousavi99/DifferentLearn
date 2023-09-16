using DifferentLearn.Core.DTOs.Order;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Order;
using ImageProcessor.Core.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DifferentLearn.Web.Areas.UserPanel.Controllers
{

    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await _orderService.GetUserOrdersAsync(User.Identity.Name));
        }
        public async Task<IActionResult> ShowOrder(int id,bool finaly=false)
        {
            Order order = await _orderService.GetOrderForUserPanelAsync(User.Identity.Name, id);
            
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.finaly=finaly;
            return View(order);
        }

        public async Task<IActionResult> FinalyOrder(int id)
        {
            if (await _orderService.FinalyOrderAsync(User.Identity.Name, id))
            {
                return Redirect("/userpanel/order/ShowOrder/" + id + "?finaly=true");
            }
            return BadRequest();
        }

        public async Task<IActionResult> UseDisCount(int orderid,string code)
        {
            DisCountUseType type = await _orderService.UseDisCountAsync(orderid, code);
            return Redirect("/userpanel/order/showorder/" + orderid + "?type=" + type.ToString());
        }
    }
}
