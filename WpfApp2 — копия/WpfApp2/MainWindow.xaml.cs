using System;
using System.Data.SQLite;
using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=database_name.db;Version=3;";
        private string userRole;

        public MainWindow()
        {
            InitializeComponent();
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

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            string licensePlate = LicensePlateTextBox.Text;
            string brand = BrandTextBox.Text;
            int year = int.Parse(YearTextBox.Text);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Автомобиль (ГосНомер, Марка, ГодВыпуска) VALUES (@ГосНомер, @Марка, @ГодВыпуска)", connection);
                command.Parameters.AddWithValue("@ГосНомер", licensePlate);
                command.Parameters.AddWithValue("@Марка", brand);
                command.Parameters.AddWithValue("@ГодВыпуска", year);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Car added successfully!");
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            int employeeNumber = int.Parse(EmployeeNumberTextBox.Text);
            string fullName = FullNameTextBox.Text;
            char licenseCategory = char.Parse(LicenseCategoryTextBox.Text);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Водитель (ТабельныйНомер, ФИО, КатегорияВодительскихПрав) VALUES (@ТабельныйНомер, @ФИО, @КатегорияВодительскихПрав)", connection);
                command.Parameters.AddWithValue("@ТабельныйНомер", employeeNumber);
                command.Parameters.AddWithValue("@ФИО", fullName);
                command.Parameters.AddWithValue("@КатегорияВодительскихПрав", licenseCategory);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Driver added successfully!");
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Рейс (ДатаВыезда, ИдентификационныйНомер, ТабельныйНомер, ДатаВозвращения, ПунктНазначения, НачальныйПробег, КонечныйПробег) VALUES (@ДатаВыезда, @ИдентификационныйНомер, @ТабельныйНомер, @ДатаВозвращения, @ПунктНазначения, @НачальныйПробег, @КонечныйПробег)", connection);
                command.Parameters.AddWithValue("@ДатаВыезда", departureDate);
                command.Parameters.AddWithValue("@ИдентификационныйНомер", carId);
                command.Parameters.AddWithValue("@ТабельныйНомер", driverId);
                command.Parameters.AddWithValue("@ДатаВозвращения", returnDate);
                command.Parameters.AddWithValue("@ПунктНазначения", destination);
                command.Parameters.AddWithValue("@НачальныйПробег", initialMileage);
                command.Parameters.AddWithValue("@КонечныйПробег", finalMileage);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Trip added successfully!");
        }
    }
}
