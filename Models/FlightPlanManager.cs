using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models
{
    public class FlightPlanManager
    {
        List<FlightPlan> flightPlanList;

        public void RemoveFlightPlan(string id)
        {
            FlightPlan fp = flightPlanList.Where(x => x.flightID == id).FirstOrDefault();
            flightPlanList.Remove(fp);
        }
    }
}
