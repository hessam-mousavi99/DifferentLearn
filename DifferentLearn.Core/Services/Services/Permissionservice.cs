using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
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
        private DiffLearnContext _context;
        public Permissionservice(DiffLearnContext context)
        {
            _context = context;
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

        public async Task<Role> GetRoleById(int roleid)
        {
            return await _context.Roles.FindAsync(roleid);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
