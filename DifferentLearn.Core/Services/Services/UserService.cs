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

        public async Task<User> LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMD5(login.Password);
            string fixEmail=FixedText.FixEmail(login.Email);
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == fixEmail && u.Password == hashPassword);
        }

        public async Task<bool> ActiveAccount(string activecode)
        {
            var user= await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activecode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
