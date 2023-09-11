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
        Task AddRolesToUserAsync(List<int> roleids, int userid);
        Task EditRolesUserAsync(int userid, List<int> roleid);
    }
}
