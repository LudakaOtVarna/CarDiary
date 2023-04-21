using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Repositories;
using CarDiary.Data.Models;
using CarDiary.Services.Mapping;
using CarDiary.Web.ViewModels.Refuels;
using CarDiary.Web.ViewModels.Trips;

namespace CarDiary.Services
{
	public class RefuelsService : IRefuelsService
	{
		private IDeletableEntityRepository<Refuel> refuelsRepository;
		private IDeletableEntityRepository<Car> carsRepository;

		public RefuelsService(IDeletableEntityRepository<Refuel> refuelsRepository, IDeletableEntityRepository<Car> carsRepository)
		{
			this.refuelsRepository = refuelsRepository;
			this.carsRepository = carsRepository;
		}

		public async Task CreateAsync(RefuelInputModel input)
		{
			var refuel = new Refuel
			{
				Amount = input.Amount,
				Date = input.Date,
				Price = input.Price,
				CarId = input.CarId,
			};

			await this.refuelsRepository.AddAsync(refuel);
			await this.refuelsRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var refuel = this.refuelsRepository.All().FirstOrDefault(x => x.Id == id);
			this.refuelsRepository.Delete(refuel);
			await this.refuelsRepository.SaveChangesAsync();
		}
        public async Task HardDeleteAsync(int id)
        {
            var refuel = this.refuelsRepository.All().FirstOrDefault(x => x.Id == id);
            this.refuelsRepository.HardDelete(refuel);
            await this.refuelsRepository.SaveChangesAsync();
        }
        public IEnumerable<T> GetAll<T>()
		{
			var refuels = this.refuelsRepository
				.AllAsNoTracking()
				.To<T>()
				.ToList();

			return refuels;
		}

		public IEnumerable<T> GetAllWithDeleted<T>()
		{
			var refuels = this.refuelsRepository
				.AllAsNoTrackingWithDeleted()
				.To<T>()
				.ToList();

			return refuels;
		}

		public T GetById<T>(int id)
		{
			var trips = this.refuelsRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return trips;
		}
        public int Count() => this.refuelsRepository.AllAsNoTracking().Count();
        public async Task UpdateAsync(int id, RefuelInputModel input)
		{
			var refuel = this.refuelsRepository.All().FirstOrDefault(x => x.Id == id);
			var car = this.carsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CarId);
			refuel.Amount = input.Amount;
			refuel.Price = input.Price;
			refuel.Date = input.Date;
			refuel.CarId = input.CarId;
			refuel.Car = car;
			await this.refuelsRepository.SaveChangesAsync();
		}
	}
}
