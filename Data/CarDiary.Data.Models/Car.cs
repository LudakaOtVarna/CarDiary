using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Models;

using Microsoft.AspNetCore.Http;

namespace CarDiary.Data.Models
{
    public class Car : BaseDeletableModel<int>
    {
        public Car()
        {
            this.Repairs = new HashSet<Repair>();
            this.Refuels = new HashSet<Refuel>();
            this.Trips = new HashSet<Trip>();
        }
        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime Date { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

		public ICollection<Repair> Repairs { get; set; }
		public ICollection<Refuel> Refuels { get; set; }
		public ICollection<Trip> Trips { get; set; }
	}
}
