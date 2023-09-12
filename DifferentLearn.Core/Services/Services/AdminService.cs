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
        public AdminService(DiffLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<int> AddUserFromAdminAsync(CreateUserViewModel createUser)
        {
            User user = new User() {UserName=createUser.UserName,Email=createUser.Email,Password= PasswordHelper.EncodePasswordMD5(createUser.Password)};
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
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            user.IsActive = true;
            return await _userService.AddUserAsync(user);
        }

        public async Task DeleteUserAsync(int userid)
        {
           User user=await _userService.GetUserByUserIdAsync(userid);   
            user.IsDelete = true;
            await _userService.UpdateUserAsync(user);
        }

        public async Task EditUserFromAdminAsync(EditUserFromAdminViewModel editUser)
        {
            User user = await _userService.GetUserByUserIdAsync(editUser.UserId);
            user.Email = editUser.Email;
            if (editUser.Password != null)
            {
                user.Password = PasswordHelper.EncodePasswordMD5(editUser.Password);
            }
            if (editUser.UserAvatar != null)
            {
                if (editUser.AvatarName != "Default.jpg")
                {
                    string deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletepath))
                    {
                        File.Delete(deletepath);
                    }

                }
                string imagePath = "";
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await editUser.UserAvatar.CopyToAsync(stream);
                }
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UsersForAdminViewModel> GetDeleteUsersAsync(int pageid = 1, string filteremail = "", string filterusername = "")
        {
            IQueryable<User> users = _context.Users.IgnoreQueryFilters().Where(u=>u.IsDelete);

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

        public async Task<EditUserFromAdminViewModel> GetUserForShowInEditModeAsync(int userid)
        {
            return await _context.Users.Where(u => u.UserId == userid)
                .Select(u => new EditUserFromAdminViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList(),
                }).SingleAsync();
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
