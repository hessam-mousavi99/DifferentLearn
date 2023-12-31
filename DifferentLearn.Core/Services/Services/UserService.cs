﻿using DifferentLearn.Core.Convertors;
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
    public class UserService : IUserService
    {
        private DiffLearnContext _context;
        IWalletService _walletService;
        public UserService(DiffLearnContext context ,IWalletService walletService)
        {
            _context = context;
            _walletService = walletService;
        }
       
        public async Task<bool> IsExistEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<int> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> IsExistUserNameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User> LoginUserAsync(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMD5(login.Password);
            string fixEmail = FixedText.FixEmail(login.Email);
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == fixEmail && u.Password == hashPassword);
        }

        public async Task<bool> ActiveAccountAsync(string activecode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activecode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActiveCodeAsync(string activecode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activecode);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }
  

      

        public async Task<InformationUserViewModel> GetUserInformationAsync(string username)
        {
            InformationUserViewModel information=new InformationUserViewModel();
            var user= await GetUserByUserNameAsync(username);
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = await _walletService.BalanceUserWalletAsync(username);
            return information;
        }

        public async Task<SideBarPanelViewModel> GetSideBarUserPanelDataAsync(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).Select(u=> new SideBarPanelViewModel()
            {
                UserName = u.UserName,
                ImageName=u.UserAvatar,
                RegisterDate=u.RegisterDate
            }).SingleAsync();
        }

        public async Task<EditProfileViewModel> GetDataForEditProfileUserAsync(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {
                UserName = u.UserName,
                Email = u.Email,
                AvatarName = u.UserAvatar
            }).SingleAsync();
        }

        public async Task EditProfileAsync(string username, EditProfileViewModel editProfile)
        {
            if (editProfile.UserAvatar!=null) 
            {
                string imagePath = "";
                if (editProfile.UserAvatar.FileName!= "Default.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", editProfile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    editProfile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(editProfile.UserAvatar.FileName);
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", editProfile.AvatarName);

                    using (var stream=new FileStream(imagePath,FileMode.Create))
                    {
                        await editProfile.UserAvatar.CopyToAsync(stream);
                    }
                }

                var user=await GetUserByUserNameAsync(username);
                user.UserName =editProfile.UserName;
                user.Email =editProfile.Email;
                user.UserAvatar = editProfile.AvatarName;
                await UpdateUserAsync(user);
            }
        }

        public async Task<bool> CompareOldPasswordAsync( string username,string oldpassword)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMD5(oldpassword);
            return await _context.Users.AnyAsync(u => u.UserName == username && u.Password == hashOldPassword);
        
        }

        public async Task ChangeUserPasswordAsync(string username, string newPassword)
        {
            var user = await GetUserByUserNameAsync(username);
            user.Password=PasswordHelper.EncodePasswordMD5(newPassword);
            await UpdateUserAsync(user);
        }

        public async Task<User> GetUserByUserIdAsync(int userid)
        {
            return await _context.Users.FindAsync(userid);
        }

        public async Task<InformationUserViewModel> GetUserInformationAsync(int userid)
        {
            var user = await GetUserByUserIdAsync(userid);
            InformationUserViewModel info = new InformationUserViewModel();
            info.UserName = user.UserName;
            info.Email = user.Email;
            info.RegisterDate = user.RegisterDate;
            info.Wallet = await _walletService.BalanceUserWalletAsync(user.UserName);

            return info;
        }
    }
}
