using CarDiary.Common;
using CarDiary.Services;
using CarDiary.Web.ViewModels.Administration.Dashboard;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDiary.Web.Areas.Administration.Controllers
{
	[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
	[Area("Administration")]
	public class DashboardController : Controller
	{
        private ICarsService carsService;
        private IRepairsService repairsService;
        private IRefuelsService refuelsService;
        private ITripsService tripsService;
        private IUsersService usersService;

		public DashboardController(ICarsService carsService, IRepairsService repairsService, IRefuelsService refuelsService, ITripsService tripsService, IUsersService usersService)
		{
			this.carsService = carsService;
			this.repairsService = repairsService;
			this.refuelsService = refuelsService;
			this.tripsService = tripsService;
			this.usersService = usersService;
		}

        public IActionResult Index()
		{
			var carsCount = this.carsService.Count();
			var repairsCount = this.repairsService.Count();
			var refuelsCount = this.refuelsService.Count();
			var tripsCount = this.tripsService.Count();
			var usersCount = this.usersService.Count();
			var model = new IndexModel(usersCount, carsCount, repairsCount, refuelsCount, tripsCount);
            return View(model);
		}
	}
}
