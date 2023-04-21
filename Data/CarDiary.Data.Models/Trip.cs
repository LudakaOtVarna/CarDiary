using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Models;

namespace CarDiary.Data.Models
{
    public class Trip : BaseDeletableModel<int>
    {
        public string DepartureAddress { get; set; }

        public string ArrivalAddress { get; set; }

        public double Distance { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

		public int CarId { get; set; }

		public virtual Car Car { get; set; }
	}
}
