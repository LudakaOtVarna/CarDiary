using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CarDiary.Data.Models;
using Microsoft.AspNetCore.Hosting;
using CarDiary.Services;
using CarDiary.Web.ViewModels.Cars;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using CarDiary.Common;
using System.Linq;

namespace CarDiary.Web.Controllers
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
            var userId = this.GetUserId();
            var cars = this.carsService.GetAll<CarModel>().Where(x => x.CreatorId == userId);
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
        public async Task<IActionResult> Details(int id)
        {
            var userId = this.GetUserId();
            var car = this.carsService.GetById<CarDetailsModel>(id);

            if (car != null)
            {
                if (car.CreatorId == userId)
                {
                    return this.View(car);
                }
            }
            return Redirect("/Cars/AccessDenied");
        }

        [HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = this.GetUserId();
			var car = this.carsService.GetById<CarEditModel>(id);

			if(car != null) 
			{
                if (car.CreatorId == userId)
                {
                    return this.View(car);
                }
            }		
				return Redirect("/Cars/AccessDenied");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, CarEditModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var userId = this.GetUserId();
            var car = this.carsService.GetById<CarEditModel>(id);

			if (car != null)
			{
				if (car.CreatorId == userId)
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
			}
				return Redirect("/Cars/AccessDenied");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = this.GetUserId();
            var car = this.carsService.GetById<CarModel>(id);

			if (car != null)
			{
				if (car.CreatorId == userId)
				{
					await this.carsService.DeleteAsync(id);
					return this.RedirectToAction(nameof(this.Index));
				}
			}
				return Redirect("/Cars/AccessDenied");
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
