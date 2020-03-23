using DownloadableProduct.Identity.DataModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Identity
{
    public class SeedData
    {
        public void Initial(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
                roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });

            if (!roleManager.RoleExistsAsync("User").Result)
                roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                });
            var mobinUser = userManager.FindByNameAsync("09197442364").Result;
            var mahdiUser = userManager.FindByNameAsync("09212651629").Result;
            var sajjadUser = userManager.FindByNameAsync("09017684946").Result;
            if (mobinUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09197442364",
                    PhoneNumber = "09197442364",
                    FullName = "مبین حسنی"
                }, "Mobin@123"
                 ).Result;

                if (result.Succeeded)
                {
                    mobinUser = userManager.FindByNameAsync("09197442364").Result;
                    var roleAssignResult = userManager.AddToRoleAsync(mobinUser, "Admin").Result;
                }
            }
            if (mahdiUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09212651629",
                    PhoneNumber = "09212651629",
                    FullName = "مهدی حسنی"
                }, "Mahdi@123"
                 ).Result;

                if (result.Succeeded)
                {
                    mahdiUser = userManager.FindByNameAsync("09212651629").Result;
                    var roleAssignResult = userManager.AddToRoleAsync(mahdiUser, "Admin").Result;
                }
            }
            if (sajjadUser == null)
            {

                var result = userManager.CreateAsync(new User
                {
                    UserName = "09017684946",
                    PhoneNumber = "09017684946",
                    FullName = "سجاد افشاری"
                }, "Sajjad@123"
                 ).Result;

                if (result.Succeeded)
                {
                    sajjadUser = userManager.FindByNameAsync("09017684946").Result;
                    var roleAssignResult = userManager.AddToRoleAsync(sajjadUser, "Admin").Result;
                }
            }
        }
    }
}
