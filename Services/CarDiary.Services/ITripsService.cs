using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Trips;

namespace CarDiary.Services
{
	public interface ITripsService
	{
		Task CreateAsync(TripInputModel input);

		Task UpdateAsync(int id, TripInputModel input);

		T GetById<T>(int id);

		IEnumerable<T> GetAll<T>();

		IEnumerable<T> GetAllWithDeleted<T>();

		Task DeleteAsync(int id);

        Task HardDeleteAsync(int id);

        int Count();
    }
}
