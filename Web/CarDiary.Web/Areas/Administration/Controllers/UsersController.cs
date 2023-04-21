using System.Linq;
using System.Threading.Tasks;

using CarDiary.Services;
using CarDiary.Web.ViewModels.Administration.Users;
using CarDiary.Web.ViewModels.Cars;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDiary.Web.Areas.Administration.Controllers
{
	public class UsersController : BaseController
	{
		private IUsersService usersService;

		public UsersController(IUsersService usersService)
		{
			this.usersService = usersService;
		}

		public IActionResult Index()
		{
			var users = this.usersService.GetAll<UserModel>().Where(x => x.Email != "Admin@email.com");
			return View(users);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			var user = this.usersService.GetById<UserModel>(id);

			if (user != null)
			{
				if(id != null)
				await this.usersService.HardDeleteAsync(id);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Users/Error");
		}
	}
}
