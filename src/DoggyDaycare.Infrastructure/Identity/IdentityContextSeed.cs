using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoggyDaycare.Infrastructure.Identity
{
    public class IdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser { UserName = "test" };
            await userManager.CreateAsync(defaultUser, "test123");

            await roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(defaultUser, "admin");

        }
    }
}
