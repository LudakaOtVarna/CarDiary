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

namespace CarDiary.Web.ViewModels.Trips
{
	public class TripInputModel : IMapFrom<Trip>
	{
		[Required]
		public string DepartureAddress { get; set; }

		[Required]
		public string ArrivalAddress { get; set; }

		[Required]
		public double Distance { get; set; }

		[Required]
		public DateTime DepartureTime { get; set; }

		[Required]
		public DateTime ArrivalTime { get; set; }

		[Required]
		[Display(Name = "Car")]
		public int CarId { get; set; }

		public IEnumerable<KeyValuePair<string, string>> CarsOptions { get; set; }

        // --------
        public string CarCreatorId { get; set; }

        public Car Car { get; set; }
		public bool IsDeleted { get; set; }
	}
}
