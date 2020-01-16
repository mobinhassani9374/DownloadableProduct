using DownloadableProduct.Identity.DataModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Identity
{
    public class SeedData
    {
        public void Initial(RoleManager<IdentityRole> roleManager)
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
        }
    }
}
