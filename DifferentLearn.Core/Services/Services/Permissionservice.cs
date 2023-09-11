using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Permission;
using DifferentLearn.Data.Entites.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Services
{
    public class Permissionservice : IPermissionService
    {
        private IUserService _userService;
        private DiffLearnContext _context;
        public Permissionservice(DiffLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task AddPermissionsToRoleAsync(int roleid, List<int> permissions)
        {
            foreach (var permission in permissions)
            {
                await _context.RolePermission.AddAsync(new RolePermission()
                {
                    PermissionId = permission,
                    RoleId = roleid
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role.RoleId;

        }

        public async Task AddRolesToUserAsync(List<int> roleids, int userid)
        {
            foreach (int roleid in roleids)
            {
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    RoleId = roleid,
                    UserId = userid
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckPermissionAsync(int permissionid, string username)
        {
            User user = await _userService.GetUserByUserNameAsync(username);

            int userid = user.UserId;

            List<int> userroles = await _context.UserRoles.Where(r => r.UserId == userid).Select(r => r.RoleId).ToListAsync();

            if (!userroles.Any())
            {
                return false;
            }

            List<int> RolePermission=await _context.RolePermission.Where(p=>p.PermissionId==permissionid)
                .Select(p=>p.RoleId).ToListAsync();

            return RolePermission.Any(p => userroles.Contains(p));
        }

        public async Task DeleteRoleAsync(Role role)
        {
            role.IsDelete = true;
            await UpdateRoleAsync(role);
        }

        public async Task EditRolesUserAsync(int userid, List<int> roleid)
        {
            var list = await _context.UserRoles.Where(r => r.UserId == userid).ToListAsync();
            list.ForEach(r => _context.UserRoles.Remove(r));

            await AddRolesToUserAsync(roleid, userid);
        }

        public async Task<List<Permission>> GetAllPermissionAsync()
        {
            return await _context.Permission.ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleid)
        {
            return await _context.Roles.FindAsync(roleid);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<List<int>> PermissionsRoleAsync(int roleid)
        {
            return await _context.RolePermission.Where(r => r.RoleId == roleid).Select(r => r.PermissionId).ToListAsync();
        }

        public async Task UpdatePermissionsRoleAsync(int roleid, List<int> permissions)
        {
            var list = await _context.RolePermission.Where(r => r.RoleId == roleid).ToListAsync();
            list.ForEach(p => _context.RolePermission.Remove(p));

            AddPermissionsToRoleAsync(roleid, permissions);

        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
