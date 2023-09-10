using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        IUserService _userService;
        IPermissionService _permissionService;
        IAdminService _adminService;
        public CreateUserModel(IUserService userService,IPermissionService permissionService,IAdminService adminService)
        {
            _userService = userService;
            _permissionService = permissionService;
            _adminService = adminService;
        }


        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public async Task OnGet()
        {
            ViewData["Roles"]=await _permissionService.GetRolesAsync();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedRoles )
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int userid = await _adminService.AddUserFromAdminAsync(CreateUserViewModel);
            await _permissionService.AddRolesToUserAsync(SelectedRoles, userid);

            return Redirect("/Admin/Users");
        }
    }
}
