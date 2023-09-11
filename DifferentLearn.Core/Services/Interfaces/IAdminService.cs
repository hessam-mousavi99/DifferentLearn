using DifferentLearn.Core.DTOs;
using DifferentLearn.Data.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<UsersForAdminViewModel> GetUsersAsync(int pageid = 1, string filteremail = "", string filterusername = "");
        Task<UsersForAdminViewModel> GetDeleteUsersAsync(int pageid = 1, string filteremail = "", string filterusername = "");
        Task<int> AddUserFromAdminAsync(CreateUserViewModel createUser);
        Task<EditUserFromAdminViewModel> GetUserForShowInEditModeAsync(int userid);
        Task EditUserFromAdminAsync(EditUserFromAdminViewModel editUser);
        Task DeleteUserAsync(int userid);
    }
}
