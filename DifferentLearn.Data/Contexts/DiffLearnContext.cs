using DifferentLearn.Data.Entites.User;
using DifferentLearn.Data.Entites.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Contexts
{
    public class DiffLearnContext:DbContext
    {
        public DiffLearnContext(DbContextOptions<DiffLearnContext> Options):base(Options) 
        {
            
        }
        
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

    }
}
