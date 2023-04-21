using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDiary.Services
{
    public interface IUsersService
    {
        int Count();
        IEnumerable<T> GetAll<T>();

		Task HardDeleteAsync(string id);

		T GetById<T>(string id);
	}
}
