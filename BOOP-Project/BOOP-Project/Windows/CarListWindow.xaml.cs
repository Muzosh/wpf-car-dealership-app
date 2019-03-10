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
        }

        // Event handlers
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            this.OpenCarEditWindow(null);
            this.RefreshGui();
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }
        // Helpers
        private void RefreshGui()
        {

        }

        // možná nebude potřeba? protože když dataGridu nabindujeme nějaký itemSource, třeba se bude aktualizovat sám
        private void RefreshCarsDataGrid()
        {

        }

        private void OpenCarEditWindow(Guid? carID)
        {
            CarEditWindow carEditWindow = new CarEditWindow(carID);
            carEditWindow.ShowDialog();
        }
    }
}
