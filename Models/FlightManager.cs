using FlightSimulator_Web.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Cryptography;


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
            newFlight.Date_Time = model.initial_Location.Date_Time;
            newFlight.is_External = false;
            return newFlight;


        }
        //return flights with information relative to the date(longitide,latitude...)
        public List<Flight> FlightSituitionInSpecificDate(List<FlightPlan> flightList,string date)
        {
            List<Flight> flightsInDate = new List<Flight>();
            foreach(FlightPlan flightPlan in flightList)
            {
                TimeSpan flightTime = ReturnFlightTime(flightPlan);
                TimeSpan timeBetweenDateAndTakeoff = ReturnTakeoffTimePassedFromDate(date,flightPlan);
                if (timeBetweenDateAndTakeoff > flightTime || timeBetweenDateAndTakeoff< TimeSpan.Zero)
                {
                    //plane is not in the air
                    continue;
                }
                else
                {
                    Flight flighInfoRelativeToDate = MakeNewFlightRelativeToTimeInAir(timeBetweenDateAndTakeoff, flightPlan);
                    flightsInDate.Add(flighInfoRelativeToDate);
                }

            }
            return flightsInDate;
        }

        private Flight MakeNewFlightRelativeToTimeInAir(TimeSpan TimeOfFlightUntilDate, FlightPlan flightPlan)
        {
            Flight currentFlightInfo = new Flight();
            currentFlightInfo.Flight_ID = flightPlan.FlightID;
            currentFlightInfo.Passengers = flightPlan.Passengers;
            currentFlightInfo.Company_Name = flightPlan.Company_Name;
            currentFlightInfo.Date_Time = flightPlan.initial_Location.Date_Time.Add(TimeOfFlightUntilDate);

            TimeSpan timeCounter = TimeSpan.Zero;
            double previousSegmentLongitude = flightPlan.initial_Location.Longitude;
            double previousSegmentLatitude = flightPlan.initial_Location.Latitude;
            
            //go over each location until the segment is after the plane segment or end of segments
            foreach (Location location in flightPlan.segments)
            {
                timeCounter =timeCounter.Add(TimeSpan.FromSeconds(location.TimeSpan_Seconds));
                if(TimeOfFlightUntilDate.CompareTo(timeCounter) < 0)
                {
                    //time start of segment in which the plane is
                    TimeSpan previousSegmentTimeCounter = timeCounter.Subtract(TimeSpan.FromSeconds(location.TimeSpan_Seconds));
                    //calculate in what part of segment the plane is
                    double partOfSegment = (TimeOfFlightUntilDate.TotalSeconds - previousSegmentTimeCounter.TotalSeconds) / (location.TimeSpan_Seconds);
                    
                    //calculate longitude and latitude of plane relatve to the date
                    currentFlightInfo.Longitude =previousSegmentLongitude + (location.Longitude - previousSegmentLongitude) * partOfSegment;
                    currentFlightInfo.Latitude =previousSegmentLatitude + (location.Latitude - previousSegmentLatitude) * partOfSegment;
                    break;
                }
                //save previous segment location
                previousSegmentLongitude = location.Longitude;
                previousSegmentLatitude = location.Latitude;
            }

            return currentFlightInfo;
        }

        //return how much time passed from plane takeoff relative to date
        private TimeSpan ReturnTakeoffTimePassedFromDate(string date,FlightPlan flightPlan)
        {
            DateTimeOffset dateTimeFromUser =DateTimeOffset.Parse(date);
            DateTimeOffset dateTimeOfTakeoff = flightPlan.initial_Location.Date_Time;
            //substract flight initial time from the date. give the space between them. if its minus,means date is before plane takeoff.
            TimeSpan timeBetweenDates = dateTimeFromUser.Subtract(dateTimeOfTakeoff);
            return timeBetweenDates;

        }
        //returns how much time the plane is in the air
        public TimeSpan ReturnFlightTime(FlightPlan flight)
        {
            TimeSpan flightTime = new TimeSpan(0, 0, 0);
            foreach(Location segment in flight.segments)
            {
                flightTime += TimeSpan.FromSeconds(segment.TimeSpan_Seconds);
            }
            return flightTime;
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

        public string ChangeDateFormatToUTC(DateTimeOffset fullDate)
        {
            return fullDate.ToString();
        }

        public bool CheckIfFlightIsValid(Flight flight)
        {
            if (flight.Passengers < 0 || flight.Company_Name == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfFlightPlanIsValid(FlightPlan flightPlan) 
        {
            if(flightPlan.Passengers < 0 || flightPlan.Company_Name == null || flightPlan.segments == null)
            {
                return false;
            }

            return true;
        }
    }
}
