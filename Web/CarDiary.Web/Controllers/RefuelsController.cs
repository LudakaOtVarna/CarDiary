using CarDiary.Data.Models;
using CarDiary.Services;
using CarDiary.Web.ViewModels.Trips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CarDiary.Web.ViewModels.Refuels;
using CarDiary.Web.ViewModels.Cars;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CarDiary.Web.Controllers
{
	public class RefuelsController : BaseController
	{
		private IRefuelsService refuelsService;
		private ICarsService carsService;

		public RefuelsController(IRefuelsService refuelsService, ICarsService carsService)
		{
			this.refuelsService = refuelsService;
			this.carsService = carsService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var userId = this.GetUserId();
			var refuels = this.refuelsService.GetAll<RefuelModel>().Where(x => x.CarCreatorId == userId);
			return View(refuels);
		}

        [HttpGet]
		[Authorize]
		public async Task<IActionResult> Details(int id)
		{
			var userId = this.GetUserId();
			var refuels = this.refuelsService.GetAll<RefuelModel>().Where(x => x.CarCreatorId == userId && x.Car.Id == id);
			if(refuels.Count() > 0)
			{
				return View(refuels);
			}
			else
			{
				return Redirect("/Refuels/AccessDenied");
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
			var model = new RefuelInputModel { CarsOptions = options };
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(RefuelInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}
			var userId = this.GetUserId();
			if (this.carsService.GetById<CarModel>(input.CarId).CreatorId == userId)
			{
				await this.refuelsService.CreateAsync(input);
				return this.RedirectToAction(nameof(this.Index));
			}
			else
			{
				return Redirect("/Refuels/AccessDenied");
			}

		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = GetUserId();
			var refuel = this.refuelsService.GetById<RefuelInputModel>(id);

			if(refuel != null)
			{
                if (refuel.CarCreatorId == userId)
                {
                    var cars = this.carsService.GetAll<CarModel>().Where(x => x.CreatorId == userId);
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    refuel.CarsOptions = options;
                    return this.View(refuel);
                }
            }
				return Redirect("/Refuels/AccessDenied");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, RefuelInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var userId = GetUserId();
            var refuel = this.refuelsService.GetById<RefuelInputModel>(id);

            if (refuel != null)
            {
				if (refuel.CarCreatorId == userId)
				{
					await this.refuelsService.UpdateAsync(id, input);

					return this.RedirectToAction(nameof(this.Index));
				}
			} 
                return Redirect("/Refuels/AccessDenied");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = GetUserId();
            var refuel = this.refuelsService.GetById<RefuelModel>(id);
			if (refuel != null)
			{
				if (refuel.CarCreatorId == userId)
				{
					await this.refuelsService.DeleteAsync(id);
					return this.RedirectToAction(nameof(this.Index));
				}
			}
				return Redirect("/Refuels/AccessDenied");
        }
	}
}
