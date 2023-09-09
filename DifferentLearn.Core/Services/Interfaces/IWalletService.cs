using DifferentLearn.Core.DTOs;
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
    }
}
