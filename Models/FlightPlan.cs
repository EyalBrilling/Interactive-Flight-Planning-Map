using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models

    
{
    [Owned]
    public class InitialLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        public DateTimeOffset Date_Time { get; set; }
    }
    [Owned]
    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        public double TimeSpan_Seconds { get; set; }

 
    }
    public class FlightPlan
    {
        [Key]
        public string FlightID { get; set; }

        public int Passengers { get; set; }
        public string Company_Name { get; set; }

        public InitialLocation initial_Location { get; set; }

        public List<Location> segments { get; set; }

        public FlightPlan()
        {
            this.initial_Location = new InitialLocation();
            this.segments = new List<Location>();
        }

        
    }
}
