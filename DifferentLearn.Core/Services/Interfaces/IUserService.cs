using DifferentLearn.Core.DTOs;
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
        Task<bool> IsExistUserName(string username);
        Task<bool> IsExistEmail(string email);
        int AddUser(User user);
        Task<User> LoginUser(LoginViewModel login);
        Task<bool> ActiveAccount(string activecode);

    }
}
