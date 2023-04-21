using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Repositories;
using CarDiary.Data.Models;
using CarDiary.Services.Mapping;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Trips;

namespace CarDiary.Services
{
	public class TripsService : ITripsService
	{
		private IDeletableEntityRepository<Trip> tripsRepository;
		private IDeletableEntityRepository<Car> carsRepository;

		public TripsService(IDeletableEntityRepository<Trip> tripsRepository, IDeletableEntityRepository<Car> carsRepository)
		{
			this.tripsRepository = tripsRepository;
			this.carsRepository = carsRepository;
		}

		public async Task CreateAsync(TripInputModel input)
		{
			var trip = new Trip
			{
				DepartureAddress = input.DepartureAddress,
				ArrivalAddress = input.ArrivalAddress,
				Distance = input.Distance,
				DepartureTime = input.DepartureTime,
				ArrivalTime = input.ArrivalTime,
				CarId = input.CarId,
			};

			await this.tripsRepository.AddAsync(trip);
			await this.tripsRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var trip = this.tripsRepository.All().FirstOrDefault(x => x.Id == id);
			this.tripsRepository.Delete(trip);
			await this.tripsRepository.SaveChangesAsync();
		}
        public async Task HardDeleteAsync(int id)
        {
            var trip = this.tripsRepository.All().FirstOrDefault(x => x.Id == id);
            this.tripsRepository.HardDelete(trip);
            await this.tripsRepository.SaveChangesAsync();
        }
        public IEnumerable<T> GetAll<T>()
		{
			var trips = this.tripsRepository
				.AllAsNoTracking()
				.To<T>()
				.ToList();

			return trips;
		}
		public IEnumerable<T> GetAllWithDeleted<T>()
		{
			var trips = this.tripsRepository
				.AllAsNoTrackingWithDeleted()
				.To<T>()
				.ToList();

			return trips;
		}
		public T GetById<T>(int id)
		{
			var trip = this.tripsRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return trip;
		}
        public int Count() => this.tripsRepository.AllAsNoTracking().Count();
        public async Task UpdateAsync(int id, TripInputModel input)
		{
			var trip = this.tripsRepository.All().FirstOrDefault(x => x.Id == id);
			var car = this.carsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CarId);
			trip.DepartureAddress = input.DepartureAddress;
			trip.ArrivalAddress = input.ArrivalAddress;
			trip.Distance = input.Distance;
			trip.DepartureTime = input.DepartureTime;
			trip.ArrivalTime = input.ArrivalTime;
			trip.CarId = input.CarId;
			trip.Car = car;
			await this.tripsRepository.SaveChangesAsync();
		}
		public async Task Recover(int id)
		{
			var trip = this.tripsRepository.AllAsNoTrackingWithDeleted().FirstOrDefault(x => x.Id == id);
			trip.IsDeleted = false;
			if(trip.Car.IsDeleted == true)
			{
				var car = this.carsRepository.AllAsNoTrackingWithDeleted().FirstOrDefault(x => x.Id == trip.CarId);
				car.IsDeleted = false;
				await this.carsRepository.SaveChangesAsync();
			}
			await this.tripsRepository.SaveChangesAsync();
		}
	}
}
