using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IAdminService _adminservice;
        public IndexModel(IAdminService? adminService)
        {
            _adminservice = adminService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public async Task OnGet(int pageid=1, string filterusername="",string filteremail = "")
        {
            UsersForAdminViewModel = await _adminservice.GetUsersAsync(pageid,filteremail,filterusername);
        }
    }
}
