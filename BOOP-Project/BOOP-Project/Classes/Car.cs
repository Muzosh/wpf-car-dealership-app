using BOOP_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOP_Project
{
    public class Car
    {
        public Guid carID { get; private set; }
        public DateTime Added { get; private set; }

        public CarCategory CarCategory { get; set; }
        public CarType CarType { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }

        public string Brand { get; set; }
        public string CarDescription { get; set; }
        public string CarFeatures { get; set; }
        public double Kilometres { get; set; }
        public string Model { get; set; }
        public double Power { get; set; }
        public double Prize { get; set; }
        public int ModelYear { get; set; }
        public int SeatCount { get; set; }
        public DateTime LastModified { get; set; }

        public Car()
        {
            this.Added = DateTime.Now;
            this.LastModified = this.Added;
            this.carID = Guid.NewGuid();
        }
    }
}
