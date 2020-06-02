using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSimulator_Web.Models;
using FlightSimulator_Web.Controllers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Routing;

namespace FlightSimulator_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsContext flightContext;
        private FlightManager flightManager;
        private ServerManager serverManager;

        public FlightsController(FlightsContext context)
        {
            flightContext = context;
            flightManager = new FlightManager();
            serverManager = new ServerManager();
        }

        
        

        // GET(by ID): api/FlightPlan/5
        [Route("FlightPlan/{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            FlightPlan flightPlan;
            flightPlan = await flightContext.FlightsPlans.Include(r => r.initial_Location)
                .Include(r => r.segments).FirstOrDefaultAsync(x => x.FlightID == id);

            if (flightPlan == null)
            {
                return NotFound();
            }

            

            

            return flightPlan;
        }



        // POST: api/FlightPlan
        [Route("FlightPlan")]
        [HttpPost]
        public async Task<ActionResult<FlightPlan>> PostFlightPlan(FlightPlan flightPlan)
        {
            string newID = flightManager.GenerateRandomID();

            flightPlan.FlightID = newID;

           
            flightContext.FlightsPlans.Add(flightPlan);
            
            Flight flightFromFlightPlan = flightManager.CreateNewFlight(flightPlan);

            flightContext.Flights.Add(flightFromFlightPlan);
               
            await flightContext.SaveChangesAsync();

            return CreatedAtAction("GetFlightPlan", new { id = flightPlan.FlightID }, flightPlan);
        }

        // DELETE(by ID): api/FlightPlans/5
        [Route("FlightPlan/{id}")]
        [HttpDelete]
        public async Task<ActionResult<FlightPlan>> DeleteFlightPlan(string id)
        {

            FlightPlan flightPlan = await flightContext.FlightsPlans.FindAsync(id);
           
            if (flightPlan == null)
            {
                return NotFound();
            }

            Flight flight = await flightContext.Flights.FindAsync(id);
            if (flightPlan == null)
            {
                return NotFound();
            }

            flightContext.Flights.Remove(flight);
            flightContext.FlightsPlans.Remove(flightPlan);
            await flightContext.SaveChangesAsync();

            return flightPlan;
        }




        [Route("flights")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetCurrentServerFlightsByDate(string relative_to)
        {
            string uri = Request.QueryString.ToString();

            List<Flight> flightList = new List<Flight>();

            //get List of all active flights with placment in relative_to the date variable "relative_to"
            List<Flight> internalflightList = flightManager.FlightSituitionInSpecificDate(await flightContext.FlightsPlans.ToListAsync(), relative_to);

            //get list of external flights in relative to the date variable "relative_to"
            List<Flight> updatedExternalFlights =await serverManager.returnExternalFlights(await flightContext.Servers.ToListAsync(),relative_to);
            
            //define external flights as external and add them to the flight list.
            foreach(Flight externalFlight in updatedExternalFlights)
            {
                externalFlight.is_External = true;
                flightList.Add(externalFlight);
            }
            //recheck external variable in internal flights is not null and add to flightList
            foreach (Flight internalFlight in internalflightList)
            {
                Flight flightToTakeExternalVariable = await flightContext.Flights.FirstOrDefaultAsync(plane => plane.Flight_ID == internalFlight.Flight_ID);
                internalFlight.is_External = flightToTakeExternalVariable.is_External;
                flightList.Add(internalFlight);
            }

            if (uri.Contains("sync_all"))
            {
         
                
                return flightList;
            }

            else
            {
                flightList = flightList.Where(flight => flight.is_External == false).ToList();
                return flightList;
            }

           

        }

    




    }
}
