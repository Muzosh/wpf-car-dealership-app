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

        public static void UpdateActiveFilter(
            string brand,
            string model,
            CarCategory? carCategory,
            CarType? carType,
            FuelType? fuelType,
            int? prizeFrom,
            int? prizeTo,
            int? kilometresFrom,
            int? kilometresTo,
            int? modelYearFrom,
            int? modelYearTo)
        {
            // Update filters
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
        }

        public static void ApplyActiveFilter()
        {
            filteredCarList = fullCarList.Cast<Car>().ToList();

            if (!string.IsNullOrEmpty(activeFilter.Brand))
            {
                filteredCarList = filteredCarList
                    .Where(x =>
                        !string.IsNullOrEmpty(x.Brand) &&
                        x.Brand.ToLower().Contains(activeFilter.Brand.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(activeFilter.Model))
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Model.ToLower().Contains(activeFilter.Model.ToLower()))
                    .ToList();
            }

            if (activeFilter.CarCategory.HasValue)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.CarCategory == activeFilter.CarCategory.Value)
                    .ToList();
            }

            if (activeFilter.CarType.HasValue)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.CarType == activeFilter.CarType.Value)
                    .ToList();
            }

            if (activeFilter.FuelType.HasValue)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.FuelType == activeFilter.FuelType.Value)
                    .ToList();
            }

            if(activeFilter.PrizeFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Prize >= activeFilter.PrizeFrom)
                    .ToList();
            }

            if (activeFilter.PrizeTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Prize <= activeFilter.PrizeFrom)
                    .ToList();
            }

            if (activeFilter.KilometresFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Kilometres >= activeFilter.KilometresFrom)
                    .ToList();
            }

            if (activeFilter.KilometresTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Kilometres <= activeFilter.KilometresTo)
                    .ToList();
            }

            if (activeFilter.ModelYearFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.ModelYear >= activeFilter.ModelYearFrom)
                    .ToList();
            }

            if (activeFilter.ModelYearTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.ModelYear <= activeFilter.ModelYearTo)
                    .ToList();
            }
        }
    }
}
