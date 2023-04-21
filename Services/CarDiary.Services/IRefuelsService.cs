using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Refuels;

namespace CarDiary.Services
{
	public interface IRefuelsService
	{
		Task CreateAsync(RefuelInputModel input);

		Task UpdateAsync(int id, RefuelInputModel input);

		T GetById<T>(int id);

		IEnumerable<T> GetAll<T>();

		IEnumerable<T> GetAllWithDeleted<T>();

		Task DeleteAsync(int id);

        Task HardDeleteAsync(int id);

        int Count();
    }
}
