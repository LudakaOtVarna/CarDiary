using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;

namespace CarDiary.Data.Seeding
{
    internal class RefuelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Refuels.Any())
            {
                return;
            }

            await dbContext.Refuels.AddAsync(new Refuel()
            {
                Amount = 10,
                Price = 26,
                Date = new DateTime(2022, 1, 4),
                CarId = 1,
            });

            await dbContext.Refuels.AddAsync(new Refuel()
            {
                Amount = 21,
                Price = 56,
                Date = new DateTime(2022, 2, 8),
                CarId = 1,
            });



            await dbContext.SaveChangesAsync();
        }

    }
}
