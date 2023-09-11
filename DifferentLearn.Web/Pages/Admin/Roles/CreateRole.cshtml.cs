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
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            Role.IsDelete = false;
           int roleId= await _permissionService.AddRoleAsync(Role);


            return RedirectToPage("Index");
        }
    }
}
