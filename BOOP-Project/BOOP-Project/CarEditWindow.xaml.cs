﻿using BOOP_Project.Enums;
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

        // Loading of car data to boxes
        private void RefreshDataSource(Guid carID)
        {
            Car currentCar = CarList.fullCarList.Find(x => x.CarID == carID);
            if (currentCar == null)
            {
                MessageBox.Show(
                    "Někde nastala chyba, vybrané auto nebylo nalezeno.",
                    "Chyba!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            this.categoryComboBox.SelectedItem = currentCar.CarCategory;
            this.brandTextBox.Text = currentCar.Brand;
            this.modelTextBox.Text = currentCar.Model;
            this.fuelTypeComboBox.SelectedItem = currentCar.FuelType;
            this.priceTextBox.Text = currentCar.Price.ToString();
            this.modelYearTextBox.Text = currentCar.ModelYear.ToString();
            this.kilometresTextBox.Text = currentCar.Kilometres.ToString();
            this.typeComboBox.SelectedItem = currentCar.CarType;
            this.powerTextbox.Text = currentCar.Power.ToString();
            this.seatCountTextBox.Text = currentCar.SeatCount.ToString();
            this.transmissionComboBox.SelectedItem = currentCar.TransmissionType;
            this.featuresTextBox.Text = currentCar.CarFeatures;
            this.descriptionTextBox.Text = currentCar.CarDescription;
        }

        // Method for saving car 
        private int SaveCar()
        {
            Car currentCar;
            Car carToSave;

            if (passedCarID.HasValue)
            {
                currentCar = CarList.fullCarList.Find(x => x.CarID == passedCarID);
                if (currentCar == null)
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
                currentCar = new Car();
            }

            carToSave = this.ValidateGuiAndSetCarProperties(currentCar);

            if (carToSave == null)
            {
                return -1;
            }

            CarList.fullCarList.Remove(currentCar);
            CarList.fullCarList.Add(carToSave);
            return 1;
        }

        // Check if all boxes are filled with right data type
        private Car ValidateGuiAndSetCarProperties(Car currentCar)
        {
            StringBuilder sb = new StringBuilder();

            // CarCategory
            if (this.categoryComboBox.SelectedIndex != -1)
            {
                currentCar.CarCategory = (CarCategory)this.categoryComboBox.SelectedItem;
            }
            else
            {
                sb.Append("kategorie, ");
            }

            // Brand
            if (this.brandTextBox.Text != string.Empty)
            {
                currentCar.Brand = this.brandTextBox.Text;
            }
            else
            {
                sb.Append("značka, ");
            }

            // Model
            if (this.modelTextBox.Text != string.Empty)
            {
                currentCar.Model = this.modelTextBox.Text;
            }
            else
            {
                sb.Append("model, ");
            }

            // FuelType
            if (this.fuelTypeComboBox.SelectedIndex != -1)
            {
                currentCar.FuelType = (FuelType)this.fuelTypeComboBox.SelectedItem;
            }
            else
            {
                sb.Append("typ paliva, ");
            }

            // Price
            if (double.TryParse(this.priceTextBox.Text, out double result4))
            {
                currentCar.Price = result4;
            }
            else
            {
                sb.Append("cena, ");
            }

            // ModelYear
            if (int.TryParse(this.modelYearTextBox.Text, out int result2))
            {
                currentCar.ModelYear = result2;
            }
            else
            {
                sb.Append("rok výroby, ");
            }

            // CarDescription
            if (this.descriptionTextBox.Text != string.Empty)
            {
                currentCar.CarDescription = this.descriptionTextBox.Text;
            }
            else
            {
                sb.Append("popis, ");
            }

            // Kilometres
            if (double.TryParse(this.kilometresTextBox.Text, out double result1))
            {
                currentCar.Kilometres = result1;
            }
            else
            {
                sb.Append("najeté km, ");
            }

            // CarType
            if (this.typeComboBox.SelectedIndex != -1)
            {
                currentCar.CarType = (CarType)this.typeComboBox.SelectedItem;
            }
            else
            {
                sb.Append("typ, ");
            }

            // Power
            if (double.TryParse(this.powerTextbox.Text, out double result3))
            {
                currentCar.Power = result3;
            }
            else
            {
                sb.Append("výkon, ");
            }

            // SeatCount
            if (int.TryParse(this.seatCountTextBox.Text, out int result5))
            {
                currentCar.SeatCount = result5;
            }
            else
            {
                sb.Append("počet míst, ");
            }

            // TransmissionType
            if (this.transmissionComboBox.SelectedIndex != -1)
            {
                currentCar.TransmissionType = (TransmissionType)this.transmissionComboBox.SelectedItem; ;
            }
            else
            {
                sb.Append("převodovka, ");
            }

            // CarFeatures
            if (this.featuresTextBox.Text != string.Empty)
            {
                currentCar.CarFeatures = this.featuresTextBox.Text;
            }
            else
            {
                sb.Append("výbava, ");
            }

            // Add time, if modifying is valid
            if (sb.Length == 0)
            {
                currentCar.LastModified = DateTime.Now;
                return currentCar;
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

        // For writing valide text in textBox of price ... (double)
        private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("((^[0-9]+,[0-9]*$)|^[0-9]+$)");
            e.Handled = !regex.IsMatch(((TextBox)e.Source).Text + e.Text);
        }

        // For writing valide text in textBox of year ... (int)
        private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]");
            e.Handled = !regex.IsMatch(e.Text);
        }

        // Save button
        private void SaveCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SaveCar() == 1)
            {
                this.Close();
            }
        }

        // Cancel button
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Delete button
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (passedCarID.HasValue)
            {
                Car currentCar = CarList.fullCarList.Find(x => x.CarID == passedCarID);
                if (currentCar == null)
                {
                    MessageBox.Show(
                        "Někde nastala chyba, editované auto nebylo nalezeno.",
                        "Chyba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    CarList.fullCarList.Remove(currentCar);
                }

                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        //Regions
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

        private void PriceTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.priceTextBox.Text = null;
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

        #region Removing spaces
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.priceTextBox.Text.Contains(" "))
            {
                this.priceTextBox.Text = this.priceTextBox.Text.Replace(" ", "");
                this.priceTextBox.SelectionStart = this.priceTextBox.Text.Length;
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
        #endregion
    }
}
