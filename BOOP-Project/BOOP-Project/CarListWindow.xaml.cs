using BOOP_Project.Enums;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BOOP_Project
{
    /// <summary>
    /// Interaction logic for CarListWindow.xaml
    /// </summary>
    public partial class CarListWindow : Window
    {
        // TODO: Ke konci možná spojit metody ApplyAndUpdateFilters a RefreshCarsDataGrid dohromady a udělat 
        // filteredCarList private
        public CarListWindow()
        {
            InitializeComponent();
            this.carsDataGrid.ItemsSource = CarList.filteredCarList;
            this.categoryComboBox.ItemsSource = Enum.GetValues(typeof(CarCategory)).Cast<CarCategory>();
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType)).Cast<CarType>();
            this.fuelTypeComboBox.ItemsSource = Enum.GetValues(typeof(FuelType)).Cast<FuelType>();
        }

        // Event handlers
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            this.OpenCarEditWindow(null);

            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            Guid? carID;
            try
            {
                carID = ((Car)this.carsDataGrid.SelectedItem).CarID;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(
                    "Není vybrána žádná položka",
                    "Editovat položku",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            this.OpenCarEditWindow(carID.Value);

            this.carsDataGrid.SelectedItem = null;
            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void CarsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guid? carID;
            try
            {
                carID = ((Car)this.carsDataGrid.SelectedItem).CarID;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(
                    "Není vybrána žádná položka",
                    "Editovat položku",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            this.OpenCarEditWindow(carID.Value);

            this.carsDataGrid.SelectedItem = null;
            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show(
                "Chcete importované auta přidat k existujícím?" +
                "\nPokud zvolíte možnost \"ne\", pak budou stávající auta smazána.",
                "Import ze souboru",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Cancel);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                this.ImportFromCsv(true);
            }
            else if (msgBoxResult == MessageBoxResult.No)
            {
                this.ImportFromCsv(false);
            }

            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExportToCsv();
        }

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

        #region Filter event handlers
        private void BrandTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void FuelTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void PrizeFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.prizeFromTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.prizeFromTextBox.Text = this.prizeFromTextBox.Text.Replace(" ", "");
                this.prizeFromTextBox.SelectionStart = this.prizeFromTextBox.Text.Length;
            }
        }

        private void PrizeToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.prizeToTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.prizeToTextBox.Text = this.prizeToTextBox.Text.Replace(" ", "");
                this.prizeToTextBox.SelectionStart = this.prizeToTextBox.Text.Length;
            }
        }

        private void KilometresFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.kilometresFromTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.kilometresFromTextBox.Text = this.kilometresFromTextBox.Text.Replace(" ", "");
                this.kilometresFromTextBox.SelectionStart = this.kilometresFromTextBox.Text.Length;
            }
        }

        private void KilometresToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.kilometresToTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.kilometresToTextBox.Text = this.kilometresToTextBox.Text.Replace(" ", "");
                this.kilometresToTextBox.SelectionStart = this.kilometresToTextBox.Text.Length;
            }
        }

        private void ModelYearFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.modelYearFromTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.modelYearFromTextBox.Text = this.modelYearFromTextBox.Text.Replace(" ", "");
                this.modelYearFromTextBox.SelectionStart = this.modelYearFromTextBox.Text.Length;
            }
        }

        private void ModelYearToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.modelYearToTextBox.Text.Contains(" "))
            {
                this.UpdateAndApplyFilters();
            }
            else
            {
                this.modelYearToTextBox.Text = this.modelYearToTextBox.Text.Replace(" ", "");
                this.modelYearToTextBox.SelectionStart = this.modelYearToTextBox.Text.Length;
            }
        }

        private void SearchStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }
        #endregion

        #region Nulling filter event handlers
        private void CategoryComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.categoryComboBox.SelectedIndex = -1;
        }

        private void TypeComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.typeComboBox.SelectedIndex = -1;
        }

        private void FuelTypeComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.fuelTypeComboBox.SelectedIndex = -1;
        }

        private void BrandTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.brandTextBox.Text = null;
        }

        private void ModelTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.modelTextBox.Text = null;
        }

        private void PrizeFromTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.prizeFromTextBox.Text = null;
        }

        private void PrizeToTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.prizeToTextBox.Text = null;
        }

        private void KilometresFromTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.kilometresFromTextBox.Text = null;
        }

        private void KilometresToTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.kilometresToTextBox.Text = null;
        }

        private void ModelYearFromTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.modelYearFromTextBox.Text = null;
        }

        private void ModelYearToTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.modelYearToTextBox.Text = null;
        }

        private void SearchStringTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.searchStringTextBox.Text = null;
        }
        #endregion

        // Helpers
        private void RefreshCarsDataGrid()
        {
            this.carsDataGrid.ItemsSource = CarList.filteredCarList;
        }

        private void UpdateAndApplyFilters()
        {
            CarList.UpdateActiveFilter(
                this.brandTextBox.Text,
                this.modelTextBox.Text,
                (CarCategory?)this.categoryComboBox.SelectedItem,
                (CarType?)this.typeComboBox.SelectedItem,
                (FuelType?)this.fuelTypeComboBox.SelectedItem,
                double.TryParse(this.prizeFromTextBox.Text, out double result1) ? result1 : (double?)null,
                double.TryParse(this.prizeToTextBox.Text, out double result2) ? result2 : (double?)null,
                double.TryParse(this.kilometresFromTextBox.Text, out double result3) ? result3 : (double?)null,
                double.TryParse(this.kilometresToTextBox.Text, out double result4) ? result4 : (double?)null,
                int.TryParse(this.modelYearFromTextBox.Text, out int result5) ? result5 : (int?)null,
                int.TryParse(this.modelYearToTextBox.Text, out int result6) ? result6 : (int?)null,
                this.searchStringTextBox.Text);

            CarList.ApplyActiveFilter();
            this.RefreshCarsDataGrid();
        }

        private void OpenCarEditWindow(Guid? carID)
        {
            CarEditWindow carEditWindow = new CarEditWindow(carID);
            carEditWindow.ShowDialog();
        }

        private void ImportFromCsv(bool addToExistingCars)
        {
            ImportExportHelper.ImportFromCsv(addToExistingCars);
        }

        private void ExportToCsv()
        {
            ImportExportHelper.ExportToCsv();
        }
    }
}
