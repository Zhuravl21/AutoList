using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace WpfApp2
{
    public static class JsonHelper
    {
        private static readonly string UsersFilePath = "users.json";
        private static readonly string CarsFilePath = "cars.json";
        private static readonly string DriversFilePath = "drivers.json";
        private static readonly string TripsFilePath = "trips.json";

        public static List<User> LoadUsers()
        {
            return LoadData<User>(UsersFilePath);
        }

        public static void SaveUsers(List<User> users)
        {
            SaveData(users, UsersFilePath);
        }

        public static List<Car> LoadCars()
        {
            return LoadData<Car>(CarsFilePath);
        }

        public static void SaveCars(List<Car> cars)
        {
            SaveData(cars, CarsFilePath);
        }

        public static List<Driver> LoadDrivers()
        {
            return LoadData<Driver>(DriversFilePath);
        }

        public static void SaveDrivers(List<Driver> drivers)
        {
            SaveData(drivers, DriversFilePath);
        }

        public static List<Trip> LoadTrips()
        {
            return LoadData<Trip>(TripsFilePath);
        }

        public static void SaveTrips(List<Trip> trips)
        {
            SaveData(trips, TripsFilePath);
        }

        private static List<T> LoadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        private static void SaveData<T>(List<T> data, string filePath)
        {
            string jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }

}
