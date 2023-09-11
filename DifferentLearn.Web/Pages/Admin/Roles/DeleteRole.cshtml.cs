using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Roles
{
    public class DeleteRoleModel : PageModel
    {
        IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public async Task OnGet(int id)
        {
            Role=await _permissionService.GetRoleById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _permissionService.DeleteRoleAsync(Role);
            return RedirectToPage("Index");
        }
    }
}
