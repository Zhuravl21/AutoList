using System;
using System.Data.SQLite;
using System.Windows;

namespace WpfApp2
{
    public partial class LoginWindow : Window
    {
        private string connectionString = "Data Source=database_name.db;Version=3;";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Проверка пользователя
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT Role FROM Users WHERE Username = @Username AND Password = @Password", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader.GetString(0);

                    MainWindow mainWindow = new MainWindow(role);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }
    }
}
