using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator_Web.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSimulator_Web.Models
{
    public class FlightsContext :  DbContext
    {
        public FlightsContext(DbContextOptions<FlightsContext> options)
             : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightPlan> FlightsPlans { get; set; }
        public DbSet<Server> Servers { get; set; }

    }
}
