using DifferentLearn.Data.Entites.Course;
using DifferentLearn.Data.Entites.Order;
using DifferentLearn.Data.Entites.Permission;
using DifferentLearn.Data.Entites.User;
using DifferentLearn.Data.Entites.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
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


        #region Course
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DisCount> DisCounts { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(cg => !cg.IsDelete);
            modelBuilder.Entity<CourseEpisode>().HasQueryFilter(ce => !ce.IsDelete);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDelete);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(od => !od.IsDelete);
            modelBuilder.Entity<DisCount>().HasQueryFilter(d => !d.IsDelete);


            var Cascadefks = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
             .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in Cascadefks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
         
            }

            modelBuilder.Entity<CourseGroup>().HasMany(x => x.Courses).WithOne(x => x.CourseGroup);
            modelBuilder.Entity<CourseGroup>().HasMany(x => x.SubGroups).WithOne(x => x.SubCourseGroup);


           

            base.OnModelCreating(modelBuilder);


        }

    }
}
