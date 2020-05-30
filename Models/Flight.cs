using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Controllers.Models
{
    public class Flight
    {
        [Key]
        public string Flight_ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Passengers { get; set; }
        public string Company_Name { get; set; }
        public DateTime Date_Time { get; set; }
        public bool is_External { get; set; }
    }
}
