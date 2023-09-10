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

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
