using EntityFrameWork.Server.Entity.Auth;
using EntityFrameWork.Server.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,Guid,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public AppDbContext():base(nameOrConnectionString:"Default")
        {

        }

        public DbSet<AppMenu> Menus { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());

            //用户
            modelBuilder.Entity<AppUser>().HasKey(k => new { k.Id });
            modelBuilder.Entity<AppUser>().Property(k => k.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //角色
            modelBuilder.Entity<AppRole>().HasKey(k => new { k.Id });
            modelBuilder.Entity<AppRole>().Property(k => k.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppRole>().Property(k => k.Name).HasMaxLength(20).IsRequired().IsUnicode();

            //用户角色
            modelBuilder.Entity<AppUserRole>().HasKey(k => new { k.RoleId, k.UserId });
            modelBuilder.Entity<AppUserRole>().HasRequired(k => k.AppRole).WithMany(k => k.AppUserRole).HasForeignKey(k => k.RoleId).WillCascadeOnDelete(true);
            modelBuilder.Entity<AppUserRole>().HasRequired(k => k.AppUser).WithMany(k => k.AppUserRole).HasForeignKey(k => k.UserId).WillCascadeOnDelete(true);

            //菜单 属性
            modelBuilder.Entity<AppMenu>().HasKey(k => k.ID);
            modelBuilder.Entity<AppMenu>().Property(k => k.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppMenu>().Property(p => p.Name).IsUnicode().IsRequired().HasMaxLength(20);
            modelBuilder.Entity<AppMenu>().Property(p => p.MenuLink).IsUnicode().IsRequired().HasMaxLength(50);
            modelBuilder.Entity<AppMenu>().Property(p => p.MenuIcon).IsUnicode().HasMaxLength(20);
            modelBuilder.Entity<AppMenu>().Property(p => p.ActionName).IsUnicode().IsRequired().HasMaxLength(20);
            modelBuilder.Entity<AppMenu>().Property(p => p.ControlName).IsUnicode().IsRequired().HasMaxLength(20);

            //菜单关系  自连接
            modelBuilder.Entity<AppMenu>().HasMany(k => k.ChildrenMenu).WithOptional().HasForeignKey(k => k.ParentID);

            //角色菜单
            modelBuilder.Entity<AppRoleMenu>().HasKey(k=>new { k.MenuID,k.RoleID });
            modelBuilder.Entity<AppRoleMenu>().HasRequired(m => m.AppMenu).WithMany(m => m.RoleMenu).HasForeignKey(k => k.MenuID);//.WillCascadeOnDelete(true);
            modelBuilder.Entity<AppRoleMenu>().HasRequired(m => m.AppRole).WithMany(m => m.AppRoleMenu).HasForeignKey(k => k.RoleID);//.WillCascadeOnDelete(true);

           

           
        }
    }
}

