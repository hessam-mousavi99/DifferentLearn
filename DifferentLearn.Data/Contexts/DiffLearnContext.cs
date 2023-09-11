using DifferentLearn.Data.Entites.Permission;
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

        #region Permission
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        #endregion

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            base.OnModelCreating(modelBuilder);
        }

    }
}
