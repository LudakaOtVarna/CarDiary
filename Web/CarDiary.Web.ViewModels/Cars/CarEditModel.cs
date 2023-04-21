using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDiary.Data.Models;

using CarDiary.Services.Mapping;

using Microsoft.AspNetCore.Http;

namespace CarDiary.Web.ViewModels.Cars
{
	public class CarEditModel : IMapFrom<Car>
	{
		[Required]
		[MinLength(3)]
		[MaxLength(10)]
		public string Brand { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(10)]
		public string Model { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public string ImageName { get; set; }

		public IFormFile Image { get; set; }

        public string CreatorId { get; set; }
    }
}
