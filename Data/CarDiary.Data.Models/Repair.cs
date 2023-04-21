using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Common.Models;

namespace CarDiary.Data.Models
{
    public class Repair : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
