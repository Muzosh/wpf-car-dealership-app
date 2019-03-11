using System;
using System.Windows;
using System.Windows.Controls;

namespace BOOP_Project
{
    /// <summary>
    /// Interaction logic for CarListWindow.xaml
    /// </summary>
    public partial class CarListWindow : Window
    {
        public CarListWindow()
        {
            InitializeComponent();

            // dočasné, pro kontrolu
            CarList.carList.Add(new Car());
            CarList.carList[0].Brand = "Tesla";
            CarList.carList[0].Model = "Model S";
            CarList.carList.Add(new Car());
            CarList.carList[1].Brand = "BMW";
            CarList.carList[1].Model = "M3";
            this.carsDataGrid.ItemsSource = CarList.carList;
        }

        // Event handlers
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            this.OpenCarEditWindow(null);
            this.RefreshGui();
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
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            // pro kontrolu
            CarList.carList[0].Model = "zmeneno";
            this.RefreshCarsDataGrid();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        // Helpers
        private void RefreshGui()
        {

        }

        private void RefreshCarsDataGrid()
        {
            this.carsDataGrid.Items.Refresh();
        }

        private void UpdateAndApplyFilters()
        {

        }

        private void OpenCarEditWindow(Guid? carID)
        {
            CarEditWindow carEditWindow = new CarEditWindow(carID);
            carEditWindow.ShowDialog();
        }
    }
}
