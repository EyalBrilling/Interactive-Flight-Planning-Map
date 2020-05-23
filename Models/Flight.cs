using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Controllers.Models
{
    public class Flight
    {
        
        public string flightID;
        public double latitude;
        public double longitude;
        public int passengers;
        public string companyName;
        public string DateTime;
        public bool isExternal;
    }
}
