using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Repairs;

namespace CarDiary.Services
{
	public interface IRepairsService
	{
		Task CreateAsync(RepairInputModel input);

		Task UpdateAsync(int id, RepairInputModel input);

		T GetById<T>(int id);

		IEnumerable<T> GetAll<T>();

		IEnumerable<T> GetAllWithDeleted<T>();

		Task DeleteAsync(int id);

        Task HardDeleteAsync(int id);

        int Count();
    }
}
