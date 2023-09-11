using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Entites.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DifferentLearn.Web.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
       private IPermissionService _permissionService;
        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public List<Role> RolesList { get; set; }
        public async Task OnGet()
        {
            RolesList = await _permissionService.GetRolesAsync();
        }
    }
}
