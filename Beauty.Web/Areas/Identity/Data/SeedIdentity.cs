﻿using Beauty.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Web.Areas.Identity.Data
{
    public class SeedIdentity : ISeedIdentity
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void EnsurePopulated(IApplicationBuilder app)
        {
            IdentityDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!_roleManager.RoleExistsAsync(RolesData.ManagerRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RolesData.ManagerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RolesData.CustomerRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dokkyoshi.com",
                    Email = "admin@dokkyoshi.com",
                    EmailConfirmed = true,
                    FirstName = "Manager",
                    LastName = "Smith"
                }, "P@ssword123").GetAwaiter().GetResult();

                ApplicationUser user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dokkyoshi.com");

                if (user != null)
                {
                    if (!_userManager.IsInRoleAsync(user, RolesData.ManagerRole).GetAwaiter().GetResult())
                    {
                        _userManager.AddToRoleAsync(user, RolesData.ManagerRole).GetAwaiter().GetResult();
                    }
                }
            }

            return;
        }
    }
}
