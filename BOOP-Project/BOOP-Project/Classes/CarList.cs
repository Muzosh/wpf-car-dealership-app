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

        private static ActiveFilter activeFilter = new ActiveFilter();

        public static void UpdateActiveFilter(
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
            int? modelYearTo,
            double? powerFrom,
            double? powerTo,
            int? seatCountFrom,
            int? seatCountTo,
            string searchStrings)
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
            activeFilter.PowerFrom = powerFrom;
            activeFilter.PowerTo = powerTo;
            activeFilter.SeatCountFrom = seatCountFrom;
            activeFilter.SeatCountTo = seatCountTo;
            activeFilter.SearchStrings = searchStrings.Split(',', ' ', ';', '.').ToList();
        }

        // Aplication of filters on fullCarList
        public static void ApplyActiveFilter()
        {
            filteredCarList = fullCarList.Cast<Car>().ToList();

            if (!string.IsNullOrEmpty(activeFilter.Brand))
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Brand.ToLower().Contains(activeFilter.Brand.ToLower()))
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

            if (activeFilter.PrizeFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Prize >= activeFilter.PrizeFrom)
                    .ToList();
            }

            if (activeFilter.PrizeTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Prize <= activeFilter.PrizeTo)
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

            if (activeFilter.PowerFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Power >= activeFilter.PowerFrom)
                    .ToList();
            }

            if (activeFilter.PowerTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.Power <= activeFilter.PowerTo)
                    .ToList();
            }

            if (activeFilter.SeatCountFrom != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.SeatCount >= activeFilter.SeatCountFrom)
                    .ToList();
            }

            if (activeFilter.SeatCountTo != null)
            {
                filteredCarList = filteredCarList
                    .Where(x => x.SeatCount <= activeFilter.SeatCountTo)
                    .ToList();
            }

            if (activeFilter.SearchStrings.Any(x => x != string.Empty))
            {
                foreach (string searchString in activeFilter.SearchStrings)
                {
                    filteredCarList = filteredCarList
                        .Where(x =>
                            x.Brand.ToLower().Contains(searchString.ToLower()) ||
                            x.Model.ToLower().Contains(searchString.ToLower()) ||
                            x.CarCategory.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.CarType.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.FuelType.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.TransmissionType.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.Prize.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.Kilometres.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.ModelYear.ToString().ToLower().Contains(searchString.ToLower()) ||
                            (!string.IsNullOrEmpty(x.CarDescription) &&
                                x.CarDescription.ToLower().Contains(searchString.ToLower())) ||
                            (!string.IsNullOrEmpty(x.CarFeatures) &&
                                x.CarFeatures.ToLower().Contains(searchString.ToLower())) ||
                            x.Power.ToString().ToLower().Contains(searchString.ToLower()) ||
                            x.SeatCount.ToString().ToLower().Contains(searchString.ToLower()))
                        .ToList();
                }
            }

            filteredCarList = filteredCarList.OrderByDescending(x => x.Added).ToList();
        }
    }
}
