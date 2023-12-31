﻿using DifferentLearn.Core.DTOs;
using DifferentLearn.Data.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
   
        Task<bool> IsExistUserNameAsync(string username);
        Task<bool> IsExistEmailAsync(string email);
        Task<int> AddUserAsync(User user);
        Task<User> LoginUserAsync(LoginViewModel login);
        Task<bool> ActiveAccountAsync(string activecode);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByActiveCodeAsync(string activecode);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByUserNameAsync(string username);
        Task<User> GetUserByUserIdAsync(int userid);
   

     
        Task<InformationUserViewModel> GetUserInformationAsync(string username);
        Task<InformationUserViewModel> GetUserInformationAsync(int userid);
        Task<SideBarPanelViewModel> GetSideBarUserPanelDataAsync(string username);
        Task<EditProfileViewModel> GetDataForEditProfileUserAsync(string username);
        Task EditProfileAsync(string username,EditProfileViewModel editProfile);
        Task<bool> CompareOldPasswordAsync(string username,string oldpassword );
        Task ChangeUserPasswordAsync(string username, string newPassword);
    
    }
}
