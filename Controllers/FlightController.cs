using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator_Web.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSimulator_Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        List<Flight> flightList;

        // GET: api/Flight
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("hello");
        }
      
        


        // GET: api/Flight/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Flight
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Flight/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
