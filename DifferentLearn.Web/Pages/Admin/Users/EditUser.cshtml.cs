using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Security;
using DifferentLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Users
{
    [PermissionChecker(1)]
    public class EditUserModel : PageModel
    {
        IAdminService _adminService;
        IPermissionService _permissionService;
        public EditUserModel(IAdminService adminService, IPermissionService permissionService)
        {
            _adminService = adminService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public EditUserFromAdminViewModel EditUserFromAdminViewModel { get; set; }
        public async Task OnGet(int id)
        {
            EditUserFromAdminViewModel = await _adminService.GetUserForShowInEditModeAsync(id);
            ViewData["Roles"] = await _permissionService.GetRolesAsync();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedRoles)
        {

            if (!ModelState.IsValid)
            {
                return Page();

            }

            await _adminService.EditUserFromAdminAsync(EditUserFromAdminViewModel);

            await _permissionService.EditRolesUserAsync(EditUserFromAdminViewModel.UserId, SelectedRoles);

            return RedirectToPage("Index");
        }
    }
}
