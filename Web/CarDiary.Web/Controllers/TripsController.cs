using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Services;

using Microsoft.AspNetCore.Authorization;
using CarDiary.Web.ViewModels.Repairs;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CarDiary.Web.ViewModels.Trips;
using CarDiary.Web.ViewModels.Cars;
using System.Linq;
using System.Collections.Generic;

namespace CarDiary.Web.Controllers
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
			var userId = this.GetUserId();
			var trips = this.tripsService.GetAll<TripModel>().Where(x => x.CarCreatorId == userId);
			return View(trips);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Details(int id)
		{
			var userId = this.GetUserId();
			var trips = this.tripsService.GetAll<TripModel>().Where(x => x.CarCreatorId == userId && x.Car.Id == id);
			if (trips.Count() > 0)
			{
				return View(trips);
			}
			else
			{
				return Redirect("/Trips/AccessDenied");
			}
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var userId = this.GetUserId();
			var cars = this.carsService.GetAll<CarModel>().Where(x => x.CreatorId == userId);
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
			var userId = this.GetUserId();
			if (this.carsService.GetById<CarModel>(input.CarId).CreatorId == userId)
			{
				await this.tripsService.CreateAsync(input);
				return this.RedirectToAction(nameof(this.Index));
			}
			else
			{
				return Redirect("/Repairs/AccessDenied");
			}
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = this.GetUserId();
			var trip = this.tripsService.GetById<TripInputModel>(id);
			
			if(trip != null)
			{
                if (trip.CarCreatorId == userId)
                {
                    var cars = this.carsService.GetAll<CarModel>().Where(x => x.CreatorId == userId);
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    trip.CarsOptions = options;
                    return this.View(trip);
                }
            }
				return Redirect("/Trips/AccessDenied");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, TripInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var userId = this.GetUserId();

            var trip = this.tripsService.GetById<TripInputModel>(id);

            if (trip != null)
            {
				if (trip.CarCreatorId == userId)
				{
					await this.tripsService.UpdateAsync(id, input);

					return this.RedirectToAction(nameof(this.Index));
				}
			}
                return Redirect("/Trips/AccessDenied");
        }

			[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = this.GetUserId();
            var trip = this.tripsService.GetById<TripInputModel>(id);

            if (trip != null)
            {
				if (trip.CarCreatorId == userId)
				{
					await this.tripsService.DeleteAsync(id);
					return this.RedirectToAction(nameof(this.Index));
				}
            }
                return Redirect("/Trips/AccessDenied");
        }
	}
}
