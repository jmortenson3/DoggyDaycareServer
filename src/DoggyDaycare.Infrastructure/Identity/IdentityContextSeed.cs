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
            var defaultUser = new ApplicationUser { Email = "admin@test.com" };
            await userManager.CreateAsync(defaultUser, "test123");

            await roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(defaultUser, "admin");

        }
    }
}
