using Amazon.Runtime.Internal;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new ApplicationRole
                {
                    Name = "Admin"
                };
                
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("Member").Result)
            {
                var role = new ApplicationRole
                {
                    Name = "Member"
                };

                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("jk@gmail.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Jeyaaa",
                    LastName = "Bolem",
                    Email = "jk@gmail.com",
                    UserName = "jeyanth21",
                    PhoneNumber = "987654322",
                };

                var result = userManager.CreateAsync(user, "!mJihyo@9597").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
        }
    }
}
