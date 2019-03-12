using BOOP_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOP_Project.Classes
{
    public class ActiveFilter
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarCategory? CarCategory { get; set; }
        public CarType? CarType { get; set; }
        public FuelType? FuelType { get; set; }
        public double? PrizeFrom { get; set; }
        public double? PrizeTo { get; set; }
        public double? KilometresFrom { get; set; }
        public double? KilometresTo { get; set; }
        public int? ModelYearFrom { get; set; }
        public int? ModelYearTo { get; set; }
    }
}
