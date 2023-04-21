using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarDiary.Data.Seeding
{
    internal class CarsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cars.Any())
            {
                return;
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await dbContext.Cars.AddAsync(new Car()
            {
                Brand = "Scoda",
                Model = "Octavia",
                Date = new DateTime(2016, 8, 3),
                ImageName = "scodaOctavia.jpg",
                CreatorId = (await userManager.FindByEmailAsync("dinko@pgmett.com")).Id,
            }); 
            
            await dbContext.SaveChangesAsync();
        }

    }
}
