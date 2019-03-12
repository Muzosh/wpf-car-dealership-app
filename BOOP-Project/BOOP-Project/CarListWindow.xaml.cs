using BOOP_Project.Enums;
using System;
using System.Linq;
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
            CarList.loadedCarList.Add(new Car());
            CarList.loadedCarList[0].Brand = "Tesla";
            CarList.loadedCarList[0].Model = "Model S";
            CarList.loadedCarList.Add(new Car());
            CarList.loadedCarList[1].Brand = "BMW";
            CarList.loadedCarList[1].Model = "M3";
            this.carsDataGrid.ItemsSource = CarList.loadedCarList;
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
            CarList.loadedCarList[0].Model = "zmeneno";
            this.categoryComboBox.ItemsSource = Enum.GetValues(typeof(CarCategory)).Cast<CarCategory>();
            this.RefreshCarsDataGrid();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            this.searchStringTextBox.Text = CarList.activeFilter.CarCategory.ToString();
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
        private void CategoryComboBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.categoryComboBox.SelectedIndex = -1;
        }

        private void TypeComboBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.typeComboBox.SelectedIndex = -1;
        }

        private void FuelTypeComboBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.fuelTypeComboBox.SelectedIndex = -1;
        }

        private void BrandTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.brandTextBox.Text = null;
        }

        private void ModelTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelTextBox.Text = null;
        }

        private void PrizeFromTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prizeFromTextBox.Text = null;
        }

        private void PrizeToTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prizeToTextBox.Text = null;
        }

        private void KilometresFromTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.kilometresFromTextBox.Text = null;
        }

        private void KilometresToTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.kilometresToTextBox.Text = null;
        }

        private void ModelYearFromTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelYearFromTextBox.Text = null;
        }

        private void ModelYearToTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.modelYearToTextBox.Text = null;
        }

        private void SearchStringTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.searchStringTextBox.Text = null;
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
            CarList.UpdateActiveFilterAndApply();
        }

        private void OpenCarEditWindow(Guid? carID)
        {
            CarEditWindow carEditWindow = new CarEditWindow(carID);
            carEditWindow.ShowDialog();
        }
    }
}
