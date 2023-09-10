using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Generator;
using DifferentLearn.Core.Security;
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
    public class AdminService : IAdminService
    {
        IUserService _userService;
        private DiffLearnContext _context;
        public AdminService(DiffLearnContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<int> AddUserFromAdminAsync(CreateUserViewModel createUser)
        {
            User user = new User();
            user.UserName = createUser.UserName;
            user.Email = createUser.Email;
            user.Password = PasswordHelper.EncodePasswordMD5(createUser.Password);
            user.RegisterDate = DateTime.Now;
            if (createUser.UserAvatar != null)
            {
                string imagePath = "";
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(createUser.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await createUser.UserAvatar.CopyToAsync(stream);
                }
            }
            user.ActiveCode=NameGenerator.GenerateUniqCode();
            user.IsActive=true;
            return await _userService.AddUserAsync(user);
        }

        public async Task<UsersForAdminViewModel> GetUsersAsync(int pageid = 1, string filteremail = "", string filterusername = "")
        {
            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrEmpty(filteremail))
            {
                users = users.Where(u => u.Email.Contains(filteremail));
            }

            if (!string.IsNullOrEmpty(filterusername))
            {
                users = users.Where(u => u.UserName.Contains(filterusername));
            }

            int take = 8;
            int skip = (pageid - 1) * take;

            UsersForAdminViewModel list = new UsersForAdminViewModel();

            list.CurrentPage = pageid;
            list.PageCount = (int)Math.Ceiling((decimal)users.Count() / (decimal)take);
            list.Users = await users.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToListAsync();
            return list;
        }

    }
}
