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
    public class UserService : IUserService
    {
        private DiffLearnContext _context;
        public UserService(DiffLearnContext context)
        {
            _context = context;
        }
        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public async Task<bool> IsExistUserName(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
