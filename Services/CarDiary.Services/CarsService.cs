using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarDiary.Data.Common.Repositories;
using CarDiary.Data.Models;
using CarDiary.Services.Mapping;
using CarDiary.Web.ViewModels.Cars;

using Microsoft.Extensions.Options;

namespace CarDiary.Services
{
    public class CarsService : ICarsService
    {
		private IDeletableEntityRepository<Car> carsRepository;
		private IDeletableEntityRepository<Trip> tripsRepository;
		private IDeletableEntityRepository<Repair> repairsRepository;
		private IDeletableEntityRepository<Refuel> refuelsRepository;

		public CarsService(IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<Car> carsRepository, IDeletableEntityRepository<Trip> tripsRepository, IDeletableEntityRepository<Repair> repairsRepository, IDeletableEntityRepository<Refuel> refuelsRepository)
		{
			this.carsRepository = carsRepository;
			this.tripsRepository = tripsRepository;
			this.refuelsRepository = refuelsRepository;
			this.repairsRepository = repairsRepository;
		}

		public async Task CreateAsync(CarCreateModel input)
        {
            var car = new Car
            {
                Brand = input.Brand,
                Model = input.Model,
                Date = input.Date,
                CreatorId = input.CreatorId,
                ImageName = input.ImageName,
            };

            await this.carsRepository.AddAsync(car);
            await this.carsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var cars = this.carsRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return cars;
        }

		public IEnumerable<T> GetAllWithDeleted<T>()
		{
			var cars = this.carsRepository
				.AllAsNoTrackingWithDeleted()
				.To<T>()
				.ToList();

			return cars;
		}

		public int Count() => this.carsRepository.AllAsNoTracking().Count();

        public T GetById<T>(int id)
        {
            var car = this.carsRepository
                .AllAsNoTracking()
                .Where(r => r.Id == id)
                .To<T>()
                .FirstOrDefault();

            return car;
        }


        public async Task UpdateAsync(int id, CarEditModel input)
        {
            var car = this.carsRepository.All().FirstOrDefault(x => x.Id == id);

            car.Model = input.Model;
            car.Brand = input.Brand;
            car.Date = input.Date;

            if (!string.IsNullOrEmpty(input.ImageName))
            {
                car.ImageName = input.ImageName;
            }


            await this.carsRepository.SaveChangesAsync();
        }

		public async Task DeleteAsync(int id)
		{
			var car = this.carsRepository.All().FirstOrDefault(x => x.Id == id);


			if (this.repairsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisRepairs = this.repairsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var repair in hisRepairs)
				{
					this.repairsRepository.Delete(repair);
				}
			}
			if (this.refuelsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisRefuels = this.refuelsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var refuel in hisRefuels)
				{
					this.refuelsRepository.Delete(refuel);
				}
			}
			if (this.tripsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisTrips = this.tripsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var trip in hisTrips)
				{
					this.tripsRepository.Delete(trip);
				}
			}

			this.carsRepository.Delete(car);
			await this.carsRepository.SaveChangesAsync();
		}

        public async Task HardDeleteAsync(int id)
        {
            var car = this.carsRepository.All().FirstOrDefault(x => x.Id == id);


			if (this.repairsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisRepairs = this.repairsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var repair in hisRepairs)
				{
					this.repairsRepository.HardDelete(repair);
				}
			}
			if (this.refuelsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisRefuels = this.refuelsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var refuel in hisRefuels)
				{
					this.refuelsRepository.HardDelete(refuel);
				}
			}
			if (this.tripsRepository.All().Where(x => x.CarId == car.Id).Any())
			{
				var hisTrips = this.tripsRepository.All().Where(x => x.CarId == car.Id);
				foreach (var trip in hisTrips)
				{
					this.tripsRepository.HardDelete(trip);
				}
			}

			this.carsRepository.HardDelete(car);
            await this.carsRepository.SaveChangesAsync();
        }
    }
}
