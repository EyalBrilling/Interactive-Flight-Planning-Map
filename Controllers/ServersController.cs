using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSimulator_Web.Models;
using System.Net.Http;

namespace FlightSimulator_Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly FlightsContext serverContext;
        

        public ServersController(FlightsContext context)
        {
            serverContext = context;
         

        }

        // GET: api/servers
        [Route("servers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            return await serverContext.Servers.ToListAsync();

        }

        // GET(by ID): api/servers/5
        [Route("servers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Server>> GetServer(string id)
        {
            Server server = await serverContext.Servers.FindAsync(id);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }



        // POST: api/servers
        [Route("servers")]
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server server)
        {
            serverContext.Servers.Add(server);
            try
            {
                await serverContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServerExists(server.serverID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServer", new { id = server.serverID }, server);
        }

        // DELETE: api/Servers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Server>> DeleteServer(string id)
        {
            Server server = await serverContext.Servers.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }

            serverContext.Servers.Remove(server);
            await serverContext.SaveChangesAsync();

            return server;
        }

        private bool ServerExists(string id)
        {
            return serverContext.Servers.Any(e => e.serverID == id);
        }

        
    }
}
