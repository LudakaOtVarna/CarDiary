namespace CarDiary.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarDiary.Common;
    using CarDiary.Data.Models;
    using CarDiary.Data.Seeding;
    using CarDiary.Data;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedAdmin(userManager, GlobalConstants.AdministratorRoleName);
        }
        private static async Task SeedAdmin(UserManager<ApplicationUser> userManager, string roleName)
        {
            var admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "plamen@pgmett.com",
                NormalizedUserName = "plamen@pgmett.com",
                Email = "plamen@pgmett.com",
                NormalizedEmail = "plamen@pgmett.com",
            };

            await userManager.CreateAsync(admin, "123Plamen");
            await userManager.AddToRoleAsync(admin, roleName);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "dinko@pgmett.com",
                NormalizedUserName = "dinko@pgmett.com",
                Email = "dinko@pgmett.com",
                NormalizedEmail = "dinko@pgmett.com",
            };

            await userManager.CreateAsync(user, "123Dinko");
        }
    }
}