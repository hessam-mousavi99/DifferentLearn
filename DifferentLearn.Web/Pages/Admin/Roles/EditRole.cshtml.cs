using DifferentLearn.Core.Services.Interfaces;
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
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            await _permissionservice.UpdateRoleAsync(Role);

            return RedirectToPage("Index");

        }
    }
}
