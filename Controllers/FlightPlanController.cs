using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private FlightPlanManager flightPlanMangaer;


        [Route("api/flights/")]
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightPlanMangaer.RemoveFlightPlan(id);
        }
    }
}
