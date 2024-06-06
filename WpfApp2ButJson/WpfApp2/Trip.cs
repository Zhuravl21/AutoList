using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Trip
    {
        public DateTime DepartureDate { get; set; }
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Destination { get; set; }
        public int InitialMileage { get; set; }
        public int FinalMileage { get; set; }
    }

}
