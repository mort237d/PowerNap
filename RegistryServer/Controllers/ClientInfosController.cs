using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;

namespace RegistryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInfosController : ControllerBase
    {
        // GET: api/ClientInfos
        [HttpGet]
        public IEnumerable<ClientInfo> Get()
        {
            return new ClientInfo[] { "value1", "value2" };
        }

        // GET: api/ClientInfos/5
        [HttpGet("{id}", Name = "Get")]
        public ClientInfo Get(int id)
        {
            return "value";
        }

        // POST: api/ClientInfos
        [HttpPost]
        public void Post([FromBody] ClientInfo value)
        {
        }

        // PUT: api/ClientInfos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ClientInfo value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
