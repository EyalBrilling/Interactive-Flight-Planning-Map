using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator_Web.Controllers.Models;
using FlightSimulator_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FlightSimulator_Web.Controllers
{
   [Route("/api")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private FlightManager flightManager;
  

        public FlightController()
        {
            this.flightManager = new FlightManager();
           
        }

        [Route("Flights/{givenDate:datetime?}")]
        [HttpGet]
        public ActionResult GetInternalFlights(string date)
        {
            return flightManager.ReturnInternalFlightsByDate(date);
        }


        [Route("FlightPlan")]
        [HttpPost]
        public ActionResult PostNewFlight(FlightPlan recivedflightPlan)
        {
            FlightPlan newFlightPlan = recivedflightPlan;
            flightManager.AddNewFlight(newFlightPlan);
            return Ok() ;
        }







    }
}
