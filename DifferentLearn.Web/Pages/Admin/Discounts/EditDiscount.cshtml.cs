using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Discounts
{
    public class EditDiscountModel : PageModel
    {
        private IOrderService _orderService;
        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DisCount DisCount { get; set; }
        public async Task OnGet(int id)
        {
            DisCount = await _orderService.GetDiscountForEditAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _orderService.UpdateDiscountAsync(DisCount);
            return RedirectToPage("Index");
        }
    }
}
