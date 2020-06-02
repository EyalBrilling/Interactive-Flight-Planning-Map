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
                string stringToGetFlight = "http://" + server.serverURL + "/api/Flights?relative_to=" + relative_to;
               HttpResponseMessage response =  await httpClient.GetAsync(stringToGetFlight);
                string jsonString =await response.Content.ReadAsStringAsync();
                List<Flight> flightsList = JsonConvert.DeserializeObject<List<Flight>>(jsonString);
                foreach(Flight externalFlight in flightsList)
                {
                    externalFlightsList.Add(externalFlight);
                }
                
            }
            return externalFlightsList;
        }
    }

} 
