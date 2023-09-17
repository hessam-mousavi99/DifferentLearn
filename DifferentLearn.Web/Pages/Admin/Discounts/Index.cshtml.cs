using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Discounts
{
    public class IndexModel : PageModel
    {
        IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public List<DisCount> DisCounts { get; set; }
        public async Task OnGet()
        {
            DisCounts=await _orderService.GetAllDiscountsAsync();
        }
    }
}
