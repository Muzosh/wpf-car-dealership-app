using BOOP_Project.Classes;
using BOOP_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOP_Project
{
    public static class CarList
    {
        public static List<Car> fullCarList = new List<Car>();

        public static List<Car> filteredCarList = new List<Car>();

        public static ActiveFilter activeFilter = new ActiveFilter();

        public static void UpdateActiveFilterAndApply(
            string brand,
            string model,
            CarCategory? carCategory,
            CarType? carType,
            FuelType? fuelType,
            double? prizeFrom,
            double? prizeTo,
            double? kilometresFrom,
            double? kilometresTo,
            int? modelYearFrom,
            int? modelYearTo)
        {
            activeFilter.Brand = brand;
            activeFilter.Model = model;
            activeFilter.CarCategory = carCategory;
            activeFilter.CarType = carType;
            activeFilter.FuelType = fuelType;
            activeFilter.PrizeFrom = prizeFrom;
            activeFilter.PrizeTo = prizeTo;
            activeFilter.KilometresFrom = kilometresFrom;
            activeFilter.KilometresTo = kilometresTo;
            activeFilter.ModelYearFrom = modelYearFrom;
            activeFilter.ModelYearTo = modelYearTo;

            filteredCarList = fullCarList.Where(x =>
                x.Brand.ToLower().Contains(activeFilter.Brand.ToLower())).ToList();
            // ... nedokonceno
        }
    }
}
