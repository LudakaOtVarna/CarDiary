namespace CarDiary.Web.Controllers
{
    using System.Diagnostics;

    using CarDiary.Services;
    using CarDiary.Web.ViewModels;
    using CarDiary.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private ICarsService carsService;
        private IRepairsService repairsService;
        private IRefuelsService refuelsService;
        private ITripsService tripsService;
        private IUsersService usersService;

        public HomeController(ICarsService carsService, IRepairsService repairsService, IRefuelsService refuelsService, ITripsService tripsService, IUsersService usersService)
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
