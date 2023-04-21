using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Refuels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace CarDiary.Web.Areas.Administration.Controllers
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
            var refuels = this.refuelsService.GetAllWithDeleted<RefuelModel>();
            return View(refuels);
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

            await this.refuelsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var refuel = this.refuelsService.GetById<RefuelInputModel>(id);

            if (refuel != null)
            {
                    var cars = this.carsService.GetAll<CarModel>();
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    refuel.CarsOptions = options;
                    return this.View(refuel);
            }
            return Redirect("/Refuels/Error");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RefuelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var refuel = this.refuelsService.GetById<RefuelInputModel>(id);

            if (refuel != null)
            {
                    await this.refuelsService.UpdateAsync(id, input);

                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Refuels/Error");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var refuel = this.refuelsService.GetById<RefuelModel>(id);
            if (refuel != null)
            {
                    await this.refuelsService.DeleteAsync(id);
                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Refuels/Error");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> HardDelete(int id)
		{
			var refuel = this.refuelsService.GetById<RefuelModel>(id);
			if (refuel != null)
			{
				await this.refuelsService.HardDeleteAsync(id);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Refuels/Error");
		}
	}
}
