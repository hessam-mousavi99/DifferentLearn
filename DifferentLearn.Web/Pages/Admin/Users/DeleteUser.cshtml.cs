using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        private IAdminService _adminService;
        public DeleteUserModel(IAdminService adminService,IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }
        public InformationUserViewModel InformationUserViewModel { get; set; }
        public async Task OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = await _userService.GetUserInformationAsync(id);
        }

        public async Task<IActionResult> OnPost(int UserId)
        {
            await _adminService.DeleteUserAsync(UserId);
            return RedirectToPage("Index");
        }
    }
}
