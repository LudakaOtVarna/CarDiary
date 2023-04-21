using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;

namespace CarDiary.Data.Seeding
{
    internal class TripsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Trips.Any())
            {
                return;
            }

            await dbContext.Trips.AddAsync(new Trip()
            {
                Distance = 358,
                ArrivalAddress = "Sofia",
                DepartureAddress = "Shumen",
                DepartureTime = new DateTime(2023, 1, 4).AddHours(4),
                ArrivalTime = new DateTime(2023, 1, 4).AddHours(7),
                CarId = 1,
            });

            await dbContext.Trips.AddAsync(new Trip()
            {
                Distance = 88,
                ArrivalAddress = "Varna",
                DepartureAddress = "Shumen",
                DepartureTime = new DateTime(2022, 5, 2).AddHours(4),
                ArrivalTime = new DateTime(2022, 5, 2).AddHours(5),
                CarId = 1,
            });


            await dbContext.SaveChangesAsync();
        }

    }
}
