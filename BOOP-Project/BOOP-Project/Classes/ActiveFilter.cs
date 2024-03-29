﻿using BOOP_Project.Enums;
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
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public double? KilometresFrom { get; set; }
        public double? KilometresTo { get; set; }
        public int? ModelYearFrom { get; set; }
        public int? ModelYearTo { get; set; }
        public double? PowerFrom { get; set; }
        public double? PowerTo { get; set; }
        public int? SeatCountFrom { get; set; }
        public int? SeatCountTo { get; set; }
        public List<string> SearchStrings { get; set; }
    }
}
