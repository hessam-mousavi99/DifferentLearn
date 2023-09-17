using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Discounts
{
    public class CreateDiscountModel : PageModel
    {
        IOrderService _orderservice;
        public CreateDiscountModel(IOrderService orderService)
        {
            _orderservice = orderService;
        }
        [BindProperty]
        public DisCount DisCount { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //DisCount.StartDate = SDate;
            //DisCount.EndDate = EDate;

            if (!ModelState.IsValid && await _orderservice.IsExistCodeAsync(DisCount.DiscountCode))
            {
                return Page(); 
            }
            await _orderservice.AddDiscountAsync(DisCount);
            return RedirectToPage("Index");
        }
        //admin/discount/creatediscount?handler=checkcode => /admin/discount/creatediscount/checkcode
        public async Task<IActionResult> OnGetCheckCode(string code)
        {
            bool item=await _orderservice.IsExistCodeAsync(code);
            return Content(item.ToString());
        }
    }
}
