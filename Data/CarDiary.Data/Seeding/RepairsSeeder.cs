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
    internal class RepairsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Repairs.Any())
            {
                return;
            }

            await dbContext.Repairs.AddAsync(new Repair()
            {
                Name = "Смяна на масло и филтри",
                Description = "На 20000км смених масло 5 литра-70лв, маслен филтър-5лв, въздушен филтър-19лв, филтър купе-15лв, горивен филтър-25лв",
                Price = 134,
                Date = new DateTime(2018, 2, 4),
                CarId = 1,
            });

            await dbContext.Repairs.AddAsync(new Repair()
            {
                Name = "Смяна на накладки и дискове",
                Description = "На 40000км смених предни накладки-80лв, задни накладки-60лв, предни дискове-100лв, задни дискове-80лв",
                Price = 320,
                Date = new DateTime(2020, 6, 2),
                CarId = 1,
            });

            await dbContext.Repairs.AddAsync(new Repair()
            {
                Name = "Смяна на масло на автоматичната скорост",
                Description = "На 60000км смених масло 2 литра-60лв, филтър-120лв",
                Price = 180,
                Date = new DateTime(2022, 1, 2),
                CarId = 1,
            });

            await dbContext.SaveChangesAsync();
        }

    }
}