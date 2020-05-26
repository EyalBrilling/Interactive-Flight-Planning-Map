using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Controllers.Models
{
    public class Flight
    {
        
        public string Flight_ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Passengers { get; set; }
        public string Company_Name;
        public DateTime Date_Time;
        public bool is_External;
    }
}
