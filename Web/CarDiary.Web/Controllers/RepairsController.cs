using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Refuels;
using CarDiary.Web.ViewModels.Repairs;
using CarDiary.Web.ViewModels.Trips;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarDiary.Web.Controllers
{
	public class RepairsController : BaseController
	{
		private ICarsService carsService;
		private IRepairsService repairsService;
		public RepairsController(ICarsService carsService, IRepairsService repairsService)
		{
			this.carsService = carsService;
			this.repairsService = repairsService;
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var userId = this.GetUserId();
			var repairs = this.repairsService.GetAll<RepairModel>().Where(x => x.CarCreatorId == userId);
			return View(repairs);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Details(int id)
		{
			var userId = this.GetUserId();
			var repairs = this.repairsService.GetAll<RepairModel>().Where(x => x.CarCreatorId == userId && x.Car.Id == id);
			if (repairs.Count() > 0)
			{
				return View(repairs);
			}
			else
			{
				return Redirect("/Repairs/AccessDenied");
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
			var model = new RepairInputModel { CarsOptions = options };
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(RepairInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var userId = this.GetUserId();
			if(this.carsService.GetById<CarModel>(input.CarId).CreatorId == userId)
			{
				await this.repairsService.CreateAsync(input);

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
			var repair = this.repairsService.GetById<RepairInputModel>(id);
			
			if(repair != null)
			{
                if (repair.CarCreatorId == userId)
                {
                    var cars = this.carsService.GetAll<CarModel>().Where(x => x.CreatorId == userId);
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    repair.CarsOptions = options;
                    return this.View(repair);
                }
            }
                return Redirect("/Repairs/AccessDenied");
        }

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, RepairInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var userId =  this.GetUserId();
            var repair = this.repairsService.GetById<RepairInputModel>(id);

			if (repair != null)
			{
				if (repair.CarCreatorId == userId)
				{
					await this.repairsService.UpdateAsync(id, input);
                    return this.RedirectToAction(nameof(this.Index));
                }
			}

            return Redirect("/Repairs/AccessDenied");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = this.GetUserId();
            var repair = this.repairsService.GetById<RepairModel>(id);
			if (repair != null)
			{
				if (repair.CarCreatorId == userId)
				{
					await this.repairsService.DeleteAsync(id);
					return this.RedirectToAction(nameof(this.Index));
				}
			}
                return Redirect("/Repairs/AccessDenied");
        }
	}
}
