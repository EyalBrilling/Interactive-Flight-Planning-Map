using FlightSimulator_Web.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace FlightSimulator_Web.Models
{
    public class FlightManager
    {
        Dictionary<Flight, FlightPlan> flightDictionary;

        public void AddNewFlight(FlightPlan model)
        {
            flightDictionary.Add(CreateNewFlight(model), CreateNewFlightPlan(model));
            
        }

        private FlightPlan CreateNewFlightPlan(FlightPlan model)
        {
            
            FlightPlan newFlightPlan = new FlightPlan();
            newFlightPlan.Passengers = model.Passengers;
            newFlightPlan.Company_Name = model.Company_Name;
           
            newFlightPlan.initial_Location.Latitude= model.initial_Location.Latitude;
            newFlightPlan.initial_Location.Longitude = model.initial_Location.Longitude;
            newFlightPlan.initial_Location.Date_Time = model.initial_Location.Date_Time;

           foreach (Location location in model.segments)
            {
               Location newLocation = new Location();
                newFlightPlan.segments.Add(newLocation);
                newLocation.Latitude = location.Latitude;
                newLocation.Longitude = location.Longitude;
                newLocation.TimeSpan_Seconds = location.TimeSpan_Seconds;

            }
            newFlightPlan.segments = model.segments;
            return newFlightPlan;
        }

        private Flight CreateNewFlight(FlightPlan model)
        {
            return null;
        }

        internal ActionResult ReturnInternalFlightsByDate(string date)
        {
            throw new NotImplementedException();
        }
    }
}
