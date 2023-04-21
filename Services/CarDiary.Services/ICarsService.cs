using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Web.ViewModels.Cars;

namespace CarDiary.Services
{
    public interface ICarsService
    {
        Task CreateAsync(CarCreateModel input);

        Task UpdateAsync(int id, CarEditModel input);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

		IEnumerable<T> GetAllWithDeleted<T>();

		Task DeleteAsync(int id);

		Task HardDeleteAsync(int id);

        int Count();
    }
}
