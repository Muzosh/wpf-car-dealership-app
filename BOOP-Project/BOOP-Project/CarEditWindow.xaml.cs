using BOOP_Project.Enums;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            this.transmissionComboBox.ItemsSource = Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>();

            this.passedCarID = carID;
            if (this.passedCarID.HasValue)
            {
                this.RefreshDataSource(this.passedCarID.Value);
            }
        }

        // Helpers
        private void RefreshDataSource(Guid carID)
        {
            Car passedCar = CarList.fullCarList.Find(x => x.CarID == carID);
            if (passedCar == null)
            {
                MessageBox.Show(
                    "Někde nastala chyba, vybrané auto nebylo nalezeno.",
                    "Chyba!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            this.categoryComboBox.SelectedItem = passedCar.CarCategory;
            this.brandTextBox.Text = passedCar.Brand;
            this.modelTextBox.Text = passedCar.Model;
            this.fuelTypeComboBox.SelectedItem = passedCar.FuelType;
            this.prizeTextBox.Text = passedCar.Prize.ToString();
            this.modelYearTextBox.Text = passedCar.ModelYear.ToString();
            this.kilometresTextBox.Text = passedCar.Kilometres.ToString();
            this.typeComboBox.SelectedItem = passedCar.CarType;
            this.powerTextbox.Text = passedCar.Power.ToString();
            this.seatCountTextBox.Text = passedCar.SeatCount.ToString();
            this.transmissionComboBox.SelectedItem = passedCar.TransmissionType;
            this.featuresTextBox.Text = passedCar.CarFeatures;
            this.descriptionTextBox.Text = passedCar.CarDescription;
        }

        private int SaveCar()
        {
            Car selectedCar;
            Car carToSave;

            if (passedCarID.HasValue)
            {
                selectedCar = CarList.fullCarList.Find(x => x.CarID == passedCarID);
                if (selectedCar == null)
                {
                    MessageBox.Show(
                        "Někde nastala chyba, editované auto nebylo nalezeno.",
                        "Chyba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return 1;
                }
            }
            else
            {
                selectedCar = new Car();
            }

            carToSave = this.ValidateGuiAndSetCarProperties(selectedCar);

            if (carToSave == null)
            {
                return -1;
            }

            CarList.fullCarList.Remove(selectedCar);
            CarList.fullCarList.Add(carToSave);
            return 1;
        }

        private Car ValidateGuiAndSetCarProperties(Car car)
        {
            StringBuilder sb = new StringBuilder();

            // CarCategory
            if (this.categoryComboBox.SelectedIndex != -1)
            {
                car.CarCategory = (CarCategory)this.categoryComboBox.SelectedItem;
            }
            else
            {
                sb.Append("kategorie, ");
            }

            // Brand
            if (this.brandTextBox.Text != string.Empty)
            {
                car.Brand = this.brandTextBox.Text;
            }
            else
            {
                sb.Append("značka, ");
            }

            // Model
            if (this.modelTextBox.Text != string.Empty)
            {
                car.Model = this.modelTextBox.Text;
            }
            else
            {
                sb.Append("model, ");
            }

            // FuelType
            if (this.fuelTypeComboBox.SelectedIndex != -1)
            {
                car.FuelType = (FuelType)this.fuelTypeComboBox.SelectedItem;
            }
            else
            {
                sb.Append("typ paliva, ");
            }

            // Prize
            if (double.TryParse(this.prizeTextBox.Text, out double result4))
            {
                car.Prize = result4;
            }
            else
            {
                sb.Append("cena, ");
            }

            // ModelYear
            if (int.TryParse(this.modelYearTextBox.Text, out int result2))
            {
                car.ModelYear = result2;
            }
            else
            {
                sb.Append("rok výroby, ");
            }

            // CarDescription
            if (this.descriptionTextBox.Text != string.Empty)
            {
                car.CarDescription = this.descriptionTextBox.Text;
            }
            else
            {
                sb.Append("popis, ");
            }

            // Kilometres
            if (double.TryParse(this.kilometresTextBox.Text, out double result1))
            {
                car.Kilometres = result1;
            }
            else
            {
                sb.Append("najeté km, ");
            }

            // CarType
            if (this.typeComboBox.SelectedIndex != -1)
            {
                car.CarType = (CarType)this.typeComboBox.SelectedItem;
            }
            else
            {
                sb.Append("typ, ");
            }

            // Power
            if (double.TryParse(this.powerTextbox.Text, out double result3))
            {
                car.Power = result3;
            }
            else
            {
                sb.Append("výkon, ");
            }

            // SeatCount
            if (int.TryParse(this.seatCountTextBox.Text, out int result5))
            {
                car.SeatCount = result5;
            }
            else
            {
                sb.Append("počet míst, ");
            }

            // TransmissionType
            if (this.transmissionComboBox.SelectedIndex != -1)
            {
                car.TransmissionType = (TransmissionType)this.transmissionComboBox.SelectedItem; ;
            }
            else
            {
                sb.Append("převodovka, ");
            }

            // CarFeatures
            if (this.featuresTextBox.Text != string.Empty)
            {
                car.CarFeatures = this.featuresTextBox.Text;
            }
            else
            {
                sb.Append("výbava, ");
            }

            if (sb.Length == 0)
            {
                car.LastModified = DateTime.Now;
                return car;
            }
            else
            {
                MessageBox.Show(
                    "Jeden nebo více zmíněných údajů chybí, prosím doplňte je:\n" +
                        sb.ToString().TrimEnd(", ".ToCharArray()),
                    "Chybějící údaje",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return null;
            }
        }

        // Event handlers
        private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("((^[0-9]+,[0-9]*$)|^[0-9]+$)");
            e.Handled = !regex.IsMatch(((TextBox)e.Source).Text + e.Text);
        }
        private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void SaveCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SaveCar() != -1)
            {
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (passedCarID.HasValue)
            {
                CarList.fullCarList.Remove(CarList.fullCarList.Find(x => x.CarID == passedCarID.Value));
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        #region Nulling filter event handlers
        private void CategoryComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.categoryComboBox.SelectedIndex = -1;
        }

        private void BrandTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.brandTextBox.Text = null;
        }

        private void ModelTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.modelTextBox.Text = null;
        }

        private void FuelTypeComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.fuelTypeComboBox.SelectedIndex = -1;
        }

        private void PrizeTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.prizeTextBox.Text = null;
        }

        private void ModelYearTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.modelYearTextBox.Text = null;
        }

        private void DescriptionTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.descriptionTextBox.Text = null;
        }

        private void KilometresTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.kilometresTextBox.Text = null;
        }

        private void TypeComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.typeComboBox.SelectedIndex = -1;
        }

        private void PowerTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.powerTextbox.Text = null;
        }

        private void SeatCountTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.seatCountTextBox.Text = null;
        }

        private void TransmissionComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.transmissionComboBox.SelectedIndex = -1;
        }

        private void FeaturesTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.featuresTextBox.Text = null;
        }
        #endregion

        private void PrizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.prizeTextBox.Text.Contains(" "))
            {
                this.prizeTextBox.Text = this.prizeTextBox.Text.Replace(" ", "");
                this.prizeTextBox.SelectionStart = this.prizeTextBox.Text.Length;
            }
        }
        private void ModelYearTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.modelYearTextBox.Text.Contains(" "))
            {
                this.modelYearTextBox.Text = this.modelYearTextBox.Text.Replace(" ", "");
                this.modelYearTextBox.SelectionStart = this.modelYearTextBox.Text.Length;
            }
        }

        private void KilometresTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.kilometresTextBox.Text.Contains(" "))
            {
                this.kilometresTextBox.Text = this.kilometresTextBox.Text.Replace(" ", "");
                this.kilometresTextBox.SelectionStart = this.kilometresTextBox.Text.Length;
            }
        }

        private void PowerTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.powerTextbox.Text.Contains(" "))
            {
                this.powerTextbox.Text = this.powerTextbox.Text.Replace(" ", "");
                this.powerTextbox.SelectionStart = this.powerTextbox.Text.Length;
            }
        }

        private void SeatCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.seatCountTextBox.Text.Contains(" "))
            {
                this.seatCountTextBox.Text = this.seatCountTextBox.Text.Replace(" ", "");
                this.seatCountTextBox.SelectionStart = this.seatCountTextBox.Text.Length;
            }
        }
    }
}
