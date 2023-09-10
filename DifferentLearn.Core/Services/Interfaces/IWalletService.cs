using DifferentLearn.Core.DTOs;
using DifferentLearn.Data.Entites.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IWalletService
    {
        Task<int> GetUserIdByUserNameAsync(string userName);
        Task<int> BalanceUserWalletAsync(string username);
        Task<List<WalletViewModel>> GetWalletUserAsync(string username);
        Task<int> ChargeWalletAsync(string username, int amount, string description, bool ispay = false);
        Task<int> AddWalletAsync(Wallet wallet);
        Task<Wallet> GetWalletByWalletIdAsync(int walletid);
        Task UpdateWalletAsync(Wallet wallet);
    }
}
