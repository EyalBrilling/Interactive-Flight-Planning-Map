using FlightSimulator_Web.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlightSimulator_Web.Models
{
    public class FlightManager
    {
        





        private FlightPlan CreateNewFlightPlan(FlightPlan model)
        {
            
           // FlightPlan newFlightPlan = new FlightPlan();
           // newFlightPlan.Passengers = model.Passengers;
           // newFlightPlan.Company_Name = model.Company_Name;
           
           // newFlightPlan.initial_Location.Latitude= model.initial_Location.Latitude;
           // newFlightPlan.initial_Location.Longitude = model.initial_Location.Longitude;
           // newFlightPlan.initial_Location.Date_Time = model.initial_Location.Date_Time;

           //foreach (Location location in model.segments)
           // {
           //    Location newLocation = new Location();
           //     newFlightPlan.segments.Add(newLocation);
           //     newLocation.Latitude = location.Latitude;
           //     newLocation.Longitude = location.Longitude;
           //     newLocation.TimeSpan_Seconds = location.TimeSpan_Seconds;

           // }
           // newFlightPlan.segments = model.segments;
            return model;
        }


        public Flight CreateNewFlight(FlightPlan model)
        {
            Flight newFlight = new Flight();

            newFlight.Flight_ID = model.FlightID;

            newFlight.Longitude = model.initial_Location.Longitude;
            newFlight.Latitude = model.initial_Location.Latitude;
            newFlight.Passengers = model.Passengers;
            newFlight.Company_Name = model.Company_Name;
            newFlight.Date_Time = model.Date_Time;
            newFlight.is_External = false;
            return newFlight;


        }



        public string GenerateRandomID()
        {
            Random random = new Random();
            string newID = string.Empty;
            //append two randomhars (A-Z)
            newID += (Convert.ToChar(random.Next(65, 90)));
            newID += (Convert.ToChar(random.Next(65, 90)));
            //apend 4 random numbers
            newID += Convert.ToString(random.Next(1000, 9999));

            return newID;
        }

        internal ActionResult ReturnInternalFlightsByDate(string date)
        {
            throw new NotImplementedException();
        }
    }
}
