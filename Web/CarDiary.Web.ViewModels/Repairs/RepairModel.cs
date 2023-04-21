using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using CarDiary.Data.Models;
using CarDiary.Services.Mapping;
using CarDiary.Web.ViewModels.Refuels;
using CarDiary.Web.ViewModels.Trips;

namespace CarDiary.Web.ViewModels.Repairs
{
	public class RepairModel : IMapFrom<Repair>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[DataType(DataType.Html)]
		public string Description { get; set; }

		public double Price { get; set; }

		public DateTime Date { get; set; }

        public string CarBrand { get; set; }
        public string CarModel { get; set; }


        // --------
        public string CarCreatorId { get; set; }

        public Car Car { get; set; }

		public bool IsDeleted { get; set; }
	}
}
