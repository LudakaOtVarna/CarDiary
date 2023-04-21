using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Repositories;
using CarDiary.Data.Models;
using CarDiary.Services.Mapping;

using Microsoft.EntityFrameworkCore;

namespace CarDiary.Services
{
    public class UsersService : IUsersService
    {
        private IDeletableEntityRepository<ApplicationUser> usersRepository;
        private IDeletableEntityRepository<Car> carsRepository;
        private IDeletableEntityRepository<Trip> tripsRepository;
        private IDeletableEntityRepository<Repair> repairsRepository;
        private IDeletableEntityRepository<Refuel> refuelsRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<Car> carsRepository, IDeletableEntityRepository<Trip> tripsRepository, IDeletableEntityRepository<Repair> repairsRepository, IDeletableEntityRepository<Refuel> refuelsRepository)
        {
            this.usersRepository = usersRepository;
            this.carsRepository = carsRepository;
            this.tripsRepository = tripsRepository;
            this.refuelsRepository = refuelsRepository;
            this.repairsRepository = repairsRepository;
        }
        public int Count() => this.usersRepository.AllAsNoTracking().Count();

        public IEnumerable<T> GetAll<T>()
        {
            var users = this.usersRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return users;
        }
		public T GetById<T>(string id)
		{
			var user = this.usersRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return user;
		}
        public async Task HardDeleteAsync(string id)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            if (this.carsRepository.All().Where(x => x.CreatorId == id).Any())
            {
				var hisCars = this.carsRepository.All().Where(x => x.CreatorId == id);
				foreach (var car in hisCars)
				{
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
				}
			}
				
			this.usersRepository.HardDelete(user);
			await this.usersRepository.SaveChangesAsync();
		}
	}
}
