using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using CarDiary.Web.ViewModels.Repairs;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace CarDiary.Web.Areas.Administration.Controllers
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
            var repairs = this.repairsService.GetAllWithDeleted<RepairModel>();
            return View(repairs);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var cars = this.carsService.GetAll<CarModel>();

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

            await this.repairsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var repair = this.repairsService.GetById<RepairInputModel>(id);

            if (repair != null)
            {
                    var cars = this.carsService.GetAll<CarModel>();
                    var options = cars.Select(a =>
                                         new {
                                             Key = a.Id,
                                             Value = a.Brand + " " + a.Model,
                                         }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
                    repair.CarsOptions = options;
                    return this.View(repair);
            }
            return Redirect("/Repairs/Error");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RepairInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var repair = this.repairsService.GetById<RepairInputModel>(id);

            if (repair != null)
            {
                    await this.repairsService.UpdateAsync(id, input);
                    return this.RedirectToAction(nameof(this.Index));
            }

            return Redirect("/Repairs/Error");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var repair = this.repairsService.GetById<RepairModel>(id);
            if (repair != null)
            {
                    await this.repairsService.DeleteAsync(id);
                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Repairs/Error");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> HardDelete(int id)
		{
			var repair = this.repairsService.GetById<RepairModel>(id);
			if (repair != null)
			{
				await this.repairsService.HardDeleteAsync(id);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Repairs/Error");
		}
	}
}
