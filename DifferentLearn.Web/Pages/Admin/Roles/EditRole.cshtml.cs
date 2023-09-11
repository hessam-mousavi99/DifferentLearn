using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Core.Services.Services;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Roles
{
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionservice;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionservice = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public async Task OnGet(int id)
        {
            Role=await _permissionservice.GetRoleById(id);
            ViewData["Permissions"] = await _permissionservice.GetAllPermissionAsync();
            ViewData["SelectedPermissions"] = await _permissionservice.PermissionsRoleAsync(id);
        }

        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            await _permissionservice.UpdateRoleAsync(Role);
            await _permissionservice.UpdatePermissionsRoleAsync(Role.RoleId,SelectedPermission);

            return RedirectToPage("Index");

        }
    }
}
