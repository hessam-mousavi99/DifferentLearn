using DifferentLearn.Core.DTOs;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Services
{
    public class WalletService : IWalletService
    {
        private DiffLearnContext _context;
        public WalletService(DiffLearnContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserIdByUserNameAsync(string userName)
        {
            var user = await _context.Users.SingleAsync(u => u.UserName == userName);
            return user.UserId;
        }
        public async Task<int> BalanceUserWalletAsync(string username)
        {
            int userId = await GetUserIdByUserNameAsync(username);

            var deposit = await _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 1 &&w.IsPay)
                .Select(w => w.Amount).ToListAsync();

            var harvest = await _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToListAsync();

            return (deposit.Sum() - harvest.Sum());

        }

        public async Task<List<WalletViewModel>> GetWalletUserAsync(string username)
        {
            int userId = await GetUserIdByUserNameAsync(username);
            return await _context.Wallets.Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel()
                {
                    Amount = w.Amount,
                    DateTime= w.CreateDate,
                    Description=w.Description,
                    Type=w.TypeId
                }).ToListAsync();
        }
    }
}
