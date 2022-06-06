using Clean.Domain.Resources.Permissions.Commons;
using Clean.Infrastructure.Datas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Seeds
{
    internal static class SeedIdentity
    {
        internal static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            var adminRole = new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                PersianName = "ادمین کل",
                NormalizedName = "admin",
                ConcurrencyStamp = DateTime.Now.Ticks.ToString()
            };

            modelBuilder.Entity<Role>().HasData(adminRole);

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Name = "مدیر سیستم",
                ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
                SecurityStamp = DateTime.Now.Ticks.ToString(),
                NormalizedUserName = "admin",
                Code = "1234",
                IsActive = true,
                IsDeleted = false
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "(admin123)");

            modelBuilder.Entity<User>().HasData(user);

            var userRole = new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = adminRole.Id
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);

            int index = 0;
            foreach (var item in new AllPermissions().Permissions)
            {
                var permissionModel = new IdentityRoleClaim<string>()
                {
                    Id = index++,
                    ClaimType = "permission",
                    ClaimValue = item,
                    RoleId = adminRole.Id
                };

                modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(permissionModel);
            }

            return modelBuilder;
        }
    }
}
