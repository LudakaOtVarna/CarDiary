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

namespace CarDiary.Web.ViewModels.Trips
{
	public class TripModel : IMapFrom<Trip>
	{
		public int Id { get; set; }

		public string DepartureAddress { get; set; }

		public string ArrivalAddress { get; set; }

		public double Distance { get; set; }

		public DateTime DepartureTime { get; set; }

		public DateTime ArrivalTime { get; set; }

        public string CarBrand { get; set; }
        public string CarModel { get; set; }


        // --------
        public string CarCreatorId { get; set; }

        public Car Car { get; set; }

		public bool IsDeleted { get; set; }

	}
}
