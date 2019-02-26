using System;
using System.Windows;
using System.Windows.Controls;

namespace BOOP_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoNothingButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Program did nothing!",
                "Success!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        
        private void InputTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(this.inputTextBox.Text, out double input))
            {
                this.resultTextBox.Text = (input * (252 / Math.PI) - 685).ToString();
            }
            else if (this.inputTextBox.Text == string.Empty)
            {
                this.resultTextBox.Text = string.Empty;
            }
            else
            {
                this.resultTextBox.Text = "INVALID INPUT";
            }
        }
    }
}
