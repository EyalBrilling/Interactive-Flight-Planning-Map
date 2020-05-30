using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator_Web.Models
{
    public class Server
    {
        [Key]
        public string serverID { get; set; }
        public string serverURL { get; set; }
}
}
