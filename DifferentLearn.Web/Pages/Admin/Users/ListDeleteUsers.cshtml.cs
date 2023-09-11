using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Users
{
    public class ListDeleteUsersModel : PageModel
    {
        private IAdminService _adminservice;
        public ListDeleteUsersModel(IAdminService? adminService)
        {
            _adminservice = adminService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public async Task OnGet(int pageid = 1, string filterusername = "", string filteremail = "")
        {
            UsersForAdminViewModel = await _adminservice.GetDeleteUsersAsync(pageid, filteremail, filterusername);
        }
    }
}
