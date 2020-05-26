using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models

    
{
    public class InitialLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date_Time { get; set; }
    }

    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double TimeSpan_Seconds { get; set; }

 
    }
    public class FlightPlan
    {

        public int Passengers { get; set; }
        public string Company_Name { get; set; }
        public string FlightID { get; set; }
        public double StartingLatitude { get; set; }
        public double StartingLongitude { get; set; }
        public DateTime Date_Time { get; set; }
        public bool IsExternal { get; set; }

        public InitialLocation initial_Location { get; set; }

        public List<Location> segments { get; set; }

        public FlightPlan()
        {
            this.initial_Location = new InitialLocation();
            this.segments = new List<Location>();
        }

        
    }
}
