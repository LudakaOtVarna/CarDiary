using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Services.Mapping;

namespace CarDiary.Web.ViewModels.Cars
{
    public class CarDetailsModel : IMapFrom<Car>
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime Date { get; set; }

        public string ImageName { get; set; }

        public string CreatorId { get; set; }

        public IEnumerable<Repair> Repairs { get; set; }
        public IEnumerable<Refuel> Refuels { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
    }
}

