using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService; 
        }


        [BindProperty]
        public Role Role { get; set; }
        public async Task OnGet()
        {
            ViewData["Permissions"]=await _permissionService.GetAllPermissionAsync();
        }
        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            Role.IsDelete = false;
           int roleId= await _permissionService.AddRoleAsync(Role);

            await _permissionService.AddPermissionsToRoleAsync(roleId, SelectedPermission);
           
            return RedirectToPage("Index");
        }
    }
}
