using BOOP_Project.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BOOP_Project
{
    public static class ImportExportHelper
    {
        private const string CsvHeader = "CarID;Added;LastModified;Brand;Model;CarCategory;" +
        "CarType;FuelType;TransmissionType;Prize;Kilometres;Power;ModelYear;SeatCount;CarFeatures;CarDescription";

        public static void ImportFromCsv(bool addToExistingCars)
        {
            // Configure save file dialog box
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "CarList"; // Default file name
            ofd.DefaultExt = ".csv"; // Default file extension
            ofd.Filter = "CSV soubory (.csv)|*.csv"; // Filter files by extension
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.RestoreDirectory = true;

            // Process open file dialog box results
            if (ofd.ShowDialog() == true)
            {
                bool opened = false;
                try
                {
                    CarList.fullCarList.Clear();
                    using (StreamReader sr =
                        new StreamReader(File.Open(ofd.FileName, FileMode.Open), Encoding.UTF8))
                    {

                        if (sr.ReadLine() != CsvHeader)
                        {
                            MessageBox.Show(
                                "Vybraný soubor neobsahuje validní hlavičku.",
                                "Chyba",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return;
                        }

                        while (!sr.EndOfStream)
                        {
                            AddCarToFullCarList(sr.ReadLine().Split(';'));
                        }
                    }

                    opened = true;
                }
                catch
                {
                    MessageBox.Show(
                        "Import se nepovedl!",
                        "Import",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                if (opened)
                {
                    MessageBox.Show(
                        "Import byl úspěšný!",
                        "Import",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }

        private static void AddCarToFullCarList(string[] carProperties)
        {
            CarList.fullCarList.Add(
                new Car(true)
                {
                    CarID = new Guid(carProperties[0]),
                    Added = Convert.ToDateTime(carProperties[1]),
                    LastModified = Convert.ToDateTime(carProperties[2]),
                    Brand = carProperties[3],
                    Model = carProperties[4],
                    CarCategory = (CarCategory)Enum.Parse(typeof(CarCategory), carProperties[5], true),
                    CarType = (CarType)Enum.Parse(typeof(CarType), carProperties[6], true),
                    FuelType = (FuelType)Enum.Parse(typeof(FuelType), carProperties[7], true),
                    TransmissionType = (TransmissionType)Enum.Parse(typeof(TransmissionType), carProperties[8], true),
                    Prize = Convert.ToDouble(carProperties[9]),
                    Kilometres = Convert.ToDouble(carProperties[10]),
                    Power = Convert.ToDouble(carProperties[11]),
                    ModelYear = Convert.ToInt32(carProperties[12]),
                    SeatCount = Convert.ToInt32(carProperties[13]),
                    CarFeatures = carProperties[14],
                    CarDescription = carProperties[15]
                });
        }

        public static void ExportToCsv()
        {
            // Configure save file dialog box
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "CarList"; // Default file name
            sfd.DefaultExt = ".csv"; // Default file extension
            sfd.Filter = "CSV soubory (.csv)|*.csv"; // Filter files by extension
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.RestoreDirectory = true;

            // Process save file dialog box results
            if (sfd.ShowDialog() == true)
            {
                bool saved = false;
                try
                {
                    using (StreamWriter sw =
                        new StreamWriter(File.Open(sfd.FileName, FileMode.Create), Encoding.UTF8))
                    {
                        sw.WriteLine(CsvHeader);
                        foreach (Car car in CarList.fullCarList.OrderByDescending(x => x.Added))
                        {
                            sw.WriteLine(
                                car.CarID + ";" +
                                car.Added + ";" +
                                car.LastModified + ";" +
                                car.Brand + ";" +
                                car.Model + ";" +
                                car.CarCategory + ";" +
                                car.CarType + ";" +
                                car.FuelType + ";" +
                                car.TransmissionType + ";" +
                                car.Prize + ";" +
                                car.Kilometres + ";" +
                                car.Power + ";" +
                                car.ModelYear + ";" +
                                car.SeatCount + ";" +
                                car.CarFeatures + ";" +
                                car.CarDescription);
                        }
                    }

                    saved = true;
                }
                catch
                {
                    MessageBox.Show(
                        "Export se nepovedl!",
                        "Export",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                if (saved)
                {
                    MessageBox.Show(
                        "Export byl úspěšný!",
                        "Export",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }
    }
}
