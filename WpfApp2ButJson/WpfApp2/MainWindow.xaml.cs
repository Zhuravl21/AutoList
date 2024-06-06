using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private string userRole;
        private List<Car> cars;
        private List<Driver> drivers;
        private List<Trip> trips;

        public MainWindow()
        {
            InitializeComponent();
            if (userRole == null)
            {
                userRole = "Администратор";
            }
            ConfigureAccess();
            LoadData();
        }

        public MainWindow(string role) : this()
        {
            userRole = role;
            ConfigureAccess();
        }

        private void ConfigureAccess()
        {
            if (userRole == "Водитель")
            {
                AddCarButton.IsEnabled = false;
                AddDriverButton.IsEnabled = false;
                AddTripButton.IsEnabled = false;
            }
            else if (userRole == "Диспетчер")
            {
                AddDriverButton.IsEnabled = false;
                AddCarButton.IsEnabled = false;
            }
        }

        private void LoadData()
        {
            cars = JsonHelper.LoadCars();
            drivers = JsonHelper.LoadDrivers();
            trips = JsonHelper.LoadTrips();

            CarsListBox.ItemsSource = cars.Select(c => $"{c.LicensePlate} - {c.Brand} ({c.Year})");
            DriversListBox.ItemsSource = drivers.Select(d => $"{d.EmployeeNumber} - {d.FullName} ({d.LicenseCategory})");
            TripsListBox.ItemsSource = trips.Select(t => $"{t.DepartureDate.ToShortDateString()} - {t.Destination}");
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            string licensePlate = LicensePlateTextBox.Text;
            string brand = BrandTextBox.Text;
            int year = int.Parse(YearTextBox.Text);

            Car newCar = new Car { LicensePlate = licensePlate, Brand = brand, Year = year };
            cars.Add(newCar);
            JsonHelper.SaveCars(cars);

            MessageBox.Show("Car added successfully!");
            LoadData();
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            int employeeNumber = int.Parse(EmployeeNumberTextBox.Text);
            string fullName = FullNameTextBox.Text;
            string licenseCategory = LicenseCategoryTextBox.Text;

            Driver newDriver = new Driver { EmployeeNumber = employeeNumber, FullName = fullName, LicenseCategory = licenseCategory };
            drivers.Add(newDriver);
            JsonHelper.SaveDrivers(drivers);

            MessageBox.Show("Driver added successfully!");
            LoadData();
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime departureDate = DateTime.Parse(DepartureDateTextBox.Text);
            int carId = int.Parse(CarIdTextBox.Text);
            int driverId = int.Parse(DriverIdTextBox.Text);
            DateTime returnDate = DateTime.Parse(ReturnDateTextBox.Text);
            string destination = DestinationTextBox.Text;
            int initialMileage = int.Parse(InitialMileageTextBox.Text);
            int finalMileage = int.Parse(FinalMileageTextBox.Text);

            Trip newTrip = new Trip
            {
                DepartureDate = departureDate,
                CarId = carId,
                DriverId = driverId,
                ReturnDate = returnDate,
                Destination = destination,
                InitialMileage = initialMileage,
                FinalMileage = finalMileage
            };
            trips.Add(newTrip);
            JsonHelper.SaveTrips(trips);

            MessageBox.Show("Trip added successfully!");
            LoadData();
        }
    }
}
