using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Repositories;
using CarDiary.Data.Models;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Repairs;
using CarDiary.Services.Mapping;

namespace CarDiary.Services
{
	public class RepairsService : IRepairsService
	{
		private IDeletableEntityRepository<Repair> repairsRepository;
		private IDeletableEntityRepository<Car> carsRepository;

		public RepairsService(IDeletableEntityRepository<Repair> repairsRepository, IDeletableEntityRepository<Car> carsRepository)
		{
			this.repairsRepository = repairsRepository;
			this.carsRepository = carsRepository;
		}

		public async Task CreateAsync(RepairInputModel input)
		{

			var repair = new Repair
			{
				Name = input.Name,
				Description = input.Description,
				Date = input.Date,
				Price = input.Price,
				CarId = input.CarId,
			};

			await this.repairsRepository.AddAsync(repair);
			await this.repairsRepository.SaveChangesAsync();
		}

		public IEnumerable<T> GetAll<T>()
		{
			var repairs = this.repairsRepository
				.AllAsNoTracking()
				.To<T>()
				.ToList();

			return repairs;
		}

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            var repairs = this.repairsRepository
                .AllAsNoTrackingWithDeleted()
                .To<T>()
                .ToList();

            return repairs;
        }
        public int Count() => this.repairsRepository.AllAsNoTracking().Count();
        public T GetById<T>(int id)
		{
			var repair = this.repairsRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return repair;
		}

		public async Task UpdateAsync(int id, RepairInputModel input)
		{
			var repair = this.repairsRepository.All().FirstOrDefault(x => x.Id == id);
			var car = this.carsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CarId);
			repair.Name = input.Name;
			repair.Description = input.Description;
			repair.Date = input.Date;
			repair.Price = input.Price;
			repair.CarId = repair.CarId;
			repair.Car = car;
			await this.repairsRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var repair = this.repairsRepository.All().FirstOrDefault(x => x.Id == id);
			this.repairsRepository.Delete(repair);
			await this.repairsRepository.SaveChangesAsync();
		}

        public async Task HardDeleteAsync(int id)
        {
            var repair = this.repairsRepository.All().FirstOrDefault(x => x.Id == id);
            this.repairsRepository.HardDelete(repair);
            await this.repairsRepository.SaveChangesAsync();
        }
    }
}
