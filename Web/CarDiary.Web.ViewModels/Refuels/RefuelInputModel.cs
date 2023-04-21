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
using CarDiary.Web.ViewModels.Trips;

namespace CarDiary.Web.ViewModels.Refuels
{
	public class RefuelInputModel : IMapFrom<Refuel>
	{
		[Required]
		public double Amount { get; set; }
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
