using BOOP_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BOOP_Project
{
    /// <summary>
    /// Interaction logic for CarEditWindow.xaml
    /// </summary>
    public partial class CarEditWindow : Window
    {
        private Guid? passedCarID;
        public CarEditWindow(Guid? carID)
        {
            InitializeComponent();
            this.categoryComboBox.ItemsSource = Enum.GetValues(typeof(CarCategory)).Cast<CarCategory>();
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType)).Cast<CarType>();
            this.fuelTypeComboBox.ItemsSource = Enum.GetValues(typeof(FuelType)).Cast<FuelType>();

            this.passedCarID = carID;
        }

        private void SaveCarButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveCar();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveCar()
        {
            Car editedCar;

            if (passedCarID.HasValue)
            {
                editedCar = CarList.fullCarList.Find(x => x.carID == passedCarID);
                CarList.fullCarList.Remove(editedCar);
            }
            else
            {
                editedCar = new Car();
            }

            editedCar = this.ValidateGuiAndSetCarProperties(editedCar);

            CarList.fullCarList.Add(editedCar);
            this.Close();
        }

        private Car ValidateGuiAndSetCarProperties(Car car)
        {
            return car;
        }
    }
}
