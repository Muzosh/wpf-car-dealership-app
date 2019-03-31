using BOOP_Project.Enums;
using System;
using System.Collections.Generic;
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
        //private CarList carList = new CarList();
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
            //this.OpenCarEditWindow(null);
            CarList.fullCarList.Add(new Car());
            CarList.fullCarList[0].Brand = "Tesla";
            CarList.fullCarList[0].Model = "Model S";
            CarList.fullCarList.Add(new Car());
            CarList.fullCarList[1].Brand = "BMW";
            CarList.fullCarList[1].Model = "M3";

            
            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            Guid? carID;
            try
            {
                carID = ((Car)this.carsDataGrid.SelectedItem).Guid;
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
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
            }
            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
            }
            this.UpdateAndApplyFilters();
            this.RefreshCarsDataGrid();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
            this.UpdateAndApplyFilters();
        }

        private void PrizeToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void KilometresFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void KilometresToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void ModelYearFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void ModelYearToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }

        private void SearchStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateAndApplyFilters();
        }
        #endregion

        #region Nulling filter event handlers
        private void CategoryComboBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.categoryComboBox.SelectedIndex = -1;
        }

        private void TypeComboBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.typeComboBox.SelectedIndex = -1;
        }

        private void FuelTypeComboBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.fuelTypeComboBox.SelectedIndex = -1;
        }

        private void BrandTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.brandTextBox.Text = null;
        }

        private void ModelTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelTextBox.Text = null;
        }

        private void PrizeFromTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prizeFromTextBox.Text = null;
        }

        private void PrizeToTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prizeToTextBox.Text = null;
        }

        private void KilometresFromTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.kilometresFromTextBox.Text = null;
        }

        private void KilometresToTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.kilometresToTextBox.Text = null;
        }

        private void ModelYearFromTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelYearFromTextBox.Text = null;
        }

        private void ModelYearToTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelYearToTextBox.Text = null;
        }

        private void SearchStringTextBox_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                int.TryParse(this.prizeFromTextBox.Text, out int result1) ? result1 : (int?)null,
                int.TryParse(this.prizeToTextBox.Text, out int result2) ? result2 : (int?)null,
                int.TryParse(this.kilometresFromTextBox.Text, out int result3) ? result3 : (int?)null,
                int.TryParse(this.kilometresToTextBox.Text, out int result4) ? result4 : (int?)null,
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
    }
}
