using FlightSimulator_Web.Controllers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models
{
    public class ServerManager { 

        private readonly HttpClient httpClient;

        public ServerManager()
        {

            httpClient = new HttpClient();
        }

        internal async Task<List<Flight>> returnExternalFlights(List<Server> serverlist,string relative_to)
        {
            List<Flight> externalFlightsList = new List<Flight>();
            foreach (Server server in serverlist)
            {
                //string stringToGetFlight = "http://localhost:" + server.serverURL + "/api/Flights?relative_to=" + relative_to;
                try
                {
                    string stringToGetFlight = server.serverURL + "/api/Flights?relative_to=" + relative_to;
                    HttpResponseMessage response = await httpClient.GetAsync("http://" + stringToGetFlight);
                    string jsonString = await response.Content.ReadAsStringAsync();



                    //try
                    //{
                    //    Flight flight = JsonConvert.DeserializeObject<Flight>(jsonString);
                    //    externalFlightsList.Add(flight);
                    //}
                    //catch
                    //{
                        List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(jsonString);
                        externalFlightsList.AddRange(flights);
                    //}
                }
                catch
                {
                    continue;
                }
                
               
                // 
               
            }
            return externalFlightsList;
        }

        internal async Task<FlightPlan> returnExternalFlightPlan(Flight externalFlight,List<Server> serverList) {

            FlightPlan flightPlan = new FlightPlan();
            foreach (Server server in serverList)
            {
               
                    string stringToGetFlightPlan = "http://" + server.serverURL + "/api/FlightPlan/" + externalFlight.flight_id;
                    HttpResponseMessage response = await httpClient.GetAsync(stringToGetFlightPlan);
                    string jsonString = await response.Content.ReadAsStringAsync();
                    if(jsonString != null)
                    {
                        flightPlan = JsonConvert.DeserializeObject<FlightPlan>(jsonString);
                        
                    }
                else
                {
                    flightPlan = null;
                }
                
            }


            return flightPlan;
        }
        
    }

} 
