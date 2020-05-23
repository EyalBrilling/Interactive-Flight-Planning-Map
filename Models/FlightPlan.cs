using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models

    
{
    public class InitialLocation
    {
        double latitude;
        double longitude;
        string DateTime;
    }
    public class FlightPlan
    {
        
        public string flightID;
        public double startingLatitude;
        public double startingLongitude; 
        public int passengers;
        public string companyName;
        public string startingDateTime;
        public bool isExternal;
        public List<InitialLocation> segments;

        
    }
}
