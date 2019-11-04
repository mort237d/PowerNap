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
        private static Dictionary<string, List<ClientInfo>> clientDictionary = new Dictionary<string, List<ClientInfo>>
        {
            {"Trustworthy dolphin", new List<ClientInfo>
            {
                new ClientInfo(2020, "128.128.128.1"),
                new ClientInfo(2021, "129.129.129.1")
            } },
            {"Total Legal Music", new List<ClientInfo>
            {
                new ClientInfo(2020, "198.198.198.1"),
                new ClientInfo(2021, "129.119.119.1")
            } },
            {"Truth.exe", new List<ClientInfo>
            {
                new ClientInfo(2020, "128.128.128.1"),
                new ClientInfo(2021, "129.129.129.1")
            } }
        };

        // GET: api/ClientInfos
        //[HttpGet]
        //public IEnumerable<ClientInfo> Get()
        //{
        //    return new ClientInfo[] { "value1", "value2" };
        //}

        // GET: api/ClientInfos/5

        [HttpGet("{id}")]
        public List<ClientInfo> Get(string id)
        {
            return clientDictionary[id];
        }

        // POST: api/ClientInfos
        [HttpPost("{id}")]
        public int Post(string id, [FromBody] ClientInfo value)
        {
            try
            {
                if (clientDictionary.ContainsKey(id))
                {
                    if (clientDictionary[id].Find(ci => ci.Equals(value)) == null)
                    {
                        clientDictionary[id].Add(value);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        // PUT: api/ClientInfos/5
        //[HttpPut("{id}")]
        //public void Put(string id, [FromBody] ClientInfo value)
        //{
        //}

        // DELETE: api/ApiWithActions/5

        [HttpDelete("{id}")]
        public int Delete(string id, [FromBody] ClientInfo value)
        {
            try
            {
                if (clientDictionary.ContainsKey(id))
                {
                    List<ClientInfo> clientInformations = clientDictionary[id];
                    ClientInfo clientInformationToRemove = clientDictionary[id].Find(ci => ci.Equals(value));
                    clientInformations.Remove(clientInformationToRemove);
                    clientDictionary[id] = clientInformations;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
