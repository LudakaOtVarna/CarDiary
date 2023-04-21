using CarDiary.Common;
using Microsoft.AspNetCore.Authorization;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace CarDiary.Web.Areas.Administration.Controllers 
{
    public class CarsController : BaseController
    {
        private ICarsService carsService;
        public IWebHostEnvironment webHostEnviroment { get; }

        public CarsController(IWebHostEnvironment webHostEnviroment, ICarsService carsService)
        {
            this.carsService = carsService;
            this.webHostEnviroment = webHostEnviroment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cars = this.carsService.GetAllWithDeleted<CarModel>();
            return this.View(cars);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CarCreateModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var userId = this.GetUserId();

            input.CreatorId = userId;
            input.ImageName = this.UploadFile(input.Image);

            await this.carsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var car = this.carsService.GetById<CarEditModel>(id);

            if (car != null)
            {
                    return this.View(car);
            }
            return Redirect("/Cars/Error");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CarEditModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var car = this.carsService.GetById<CarEditModel>(id);

            if (car != null)
            {
                    if (input.Image != null)
                    {
                        string path = this.webHostEnviroment.WebRootPath + "/images/" + input.ImageName;
                        System.IO.File.Delete(path);
                        input.ImageName = this.UploadFile(input.Image);
                    }
                    await this.carsService.UpdateAsync(id, input);
                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Cars/Error");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var car = this.carsService.GetById<CarModel>(id);

            if (car != null)
            {
                    await this.carsService.DeleteAsync(id);
                    return this.RedirectToAction(nameof(this.Index));
            }
            return Redirect("/Cars/Error");
        }

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> HardDelete(int id)
		{
			var car = this.carsService.GetById<CarModel>(id);

			if (car != null)
			{
				await this.carsService.HardDeleteAsync(id);
				return this.RedirectToAction(nameof(this.Index));
			}
			return Redirect("/Cars/Error");
		}

		private string UploadFile(IFormFile image)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDir = Path.Combine(this.webHostEnviroment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
