using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Services.Mapping;

namespace CarDiary.Web.ViewModels.Cars
{
    public class CarModel : IMapFrom<Car>
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime Date { get; set; }

        public string ImageName { get; set; }

        public string CreatorId { get; set; }

		public bool IsDeleted { get; set; }

	}
}
