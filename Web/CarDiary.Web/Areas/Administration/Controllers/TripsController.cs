using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Trips;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace CarDiary.Web.Areas.Administration.Controllers
{
    public class TripsController : BaseController
    {
        private ITripsService tripsService;
        private ICarsService carsService;

        public TripsController(ITripsService tripsService, ICarsService carsService)
        {
            this.tripsService = tripsService;
            this.carsService = carsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var trips = this.tripsService.GetAllWithDeleted<TripModel>();
            return View(trips);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var userId = this.GetUserId();
            var cars = this.carsService.GetAll<CarModel>();
            var options = cars.Select(a =>
                                 new {
                                     Key = a.Id,
                                     Value = a.Brand + " " + a.Model,
                                 }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
            var model = new TripInputModel { CarsOptions = options };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TripInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.tripsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var trip = this.tripsService.GetById<TripInputModel>(id);

            if (trip != null)
            {
                    var cars = this.carsService.GetAll<CarModel>();
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    trip.CarsOptions = options;
                    return this.View(trip);
            }
            return Redirect("/Trips/Error");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, TripInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }


            var trip = this.tripsService.GetById<TripInputModel>(id);

            if (trip != null)
            {
                    await this.tripsService.UpdateAsync(id, input);

                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Trips/Error");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var trip = this.tripsService.GetById<TripInputModel>(id);

            if (trip != null)
            {
                    await this.tripsService.DeleteAsync(id);
                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Trips/Error");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Recover(int id)
		{
			var trip = this.tripsService.GetById<TripInputModel>(id);

			if (trip != null)
			{
                trip.IsDeleted = false;
				await this.tripsService.UpdateAsync(id, trip);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Trips/Error");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> HardDelete(int id)
		{
			var trip = this.tripsService.GetById<TripInputModel>(id);

			if (trip != null)
			{
				await this.tripsService.HardDeleteAsync(id);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Trips/Error");
		}
	}
}
