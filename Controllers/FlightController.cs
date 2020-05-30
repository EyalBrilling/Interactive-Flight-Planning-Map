using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator_Web.Controllers.Models;
using FlightSimulator_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace FlightSimulator_Web.Controllers
{
   [Route("/api")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        public FlightManager flightManager;
        


        public FlightController(FlightManager flightManager)
        {
            this.flightManager = flightManager;
           
        }

        [Route("Flights/{givenDate:datetime?}")]
        [HttpGet]
        public ActionResult GetInternalFlights(string date)
        {
            return flightManager.ReturnInternalFlightsByDate(date);
        }

      

        //[Route("FlightPlan")]
        //[HttpPost]
        //public async Task<ActionResult<FlightPlan> PostFlightPlan(FlightPlan flightPlan)
        //{
            
        //    //string recivedFlightPlanString = recivedFlightPlan.ToString();
        //    //flightManager.AddNewFlight(recivedFlightPlanString);
        //    //return Ok();
        //}







    }
}
