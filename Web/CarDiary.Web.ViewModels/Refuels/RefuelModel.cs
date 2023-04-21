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

namespace CarDiary.Web.ViewModels.Refuels
{
	public class RefuelModel : IMapFrom<Refuel>
	{
		public int Id { get; set; }
		public double Amount { get; set; }
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
