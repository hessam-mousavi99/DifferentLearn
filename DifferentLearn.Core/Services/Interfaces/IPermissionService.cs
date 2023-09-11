using DifferentLearn.Data.Entites.Permission;
using DifferentLearn.Data.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<List<Role>> GetRolesAsync();
        Task<int> AddRoleAsync(Role role);
        Task AddRolesToUserAsync(List<int> roleids, int userid);
        Task EditRolesUserAsync(int userid, List<int> roleid);
        Task<Role> GetRoleById(int roleid);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);

        Task<List<Permission>> GetAllPermissionAsync();
        Task AddPermissionsToRoleAsync(int roleid, List<int> permissions);
        Task<List<int>> PermissionsRoleAsync(int roleid);
        Task UpdatePermissionsRoleAsync(int roleid, List<int> permissions);
    }
}
