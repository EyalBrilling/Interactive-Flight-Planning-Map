using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSimulator_Web.Models;
using FlightSimulator_Web.Controllers.Models;

namespace FlightSimulator_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsContext flightContext;
        private FlightManager flightManager;

        public FlightsController(FlightsContext context)
        {
            flightContext = context;
            flightManager = new FlightManager();
        }

        
        

        // GET(by ID): api/FlightPlans/5
        [Route("FlightPlan/{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            FlightPlan flightPlan;
            flightPlan = await flightContext.FlightsPlans.FindAsync(id);

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

       
    }
}
