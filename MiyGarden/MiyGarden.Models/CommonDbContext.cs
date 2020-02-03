//using System.Data.Entity;
//using BaseMVC.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using BaseMVC.Models.TestModels;

//namespace MiyGarden.Models
//{
//    public class ApplicationUserStore : UserStore<User, Role, string, UserLogin, UserRole, UserClaim>, IUserStore<User, string>, IDisposable
//    {
//        public ApplicationUserStore() : this(new IdentityDbContext())
//        {
//            base.DisposeContext = true;
//        }

//        public ApplicationUserStore(DbContext context)
//            : base(context)
//        {
//        }
//    }

//    public class CommonDbContext : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim>
//    {
//        public CommonDbContext()
//            : base("LocalConnection")
//        {

//        }

//        public DbSet<Performance> Performance { get; set; }
//        public DbSet<TestModels.Dapper> Dapper { get; set; }
        

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<User>().ToTable("Users");
//            modelBuilder.Entity<Role>().ToTable("Roles");
//            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
//            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
//            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
//        }

//        public static CommonDbContext Create()
//        {
//            return new CommonDbContext();
//        }
//    }
//}