using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoMapper;

using CarDiary.Data.Models;
using CarDiary.Services.Mapping;
using CarDiary.Web.ViewModels.Refuels;

namespace CarDiary.Web.ViewModels.Repairs
{
	public class RepairInputModel : IMapFrom<Repair>
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(500)]
		[MinLength(10)]
		[DataType(DataType.Html)]
		public string Description { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		[Display(Name = "Car")]
		public int CarId { get; set; }

		public IEnumerable<KeyValuePair<string, string>> CarsOptions { get; set; }


		// --------
		public string CarCreatorId { get; set; }

		public Car Car { get; set; }
    }
}
