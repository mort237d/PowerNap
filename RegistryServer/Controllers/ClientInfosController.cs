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

        [HttpGet("{fileName}")]
        public List<ClientInfo> Get(string fileName)
        {
            return clientDictionary[fileName];
        }

        // POST: api/ClientInfos/music.exe
        [HttpPost("{fileName}")]
        public int Post(string fileName, [FromBody] ClientInfo value)
        {
            try
            {
                if (clientDictionary.ContainsKey(fileName))
                {
                    if (clientDictionary[fileName].Find(ci => ci.Equals(value)) == null)
                    {
                        clientDictionary[fileName].Add(value);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (!clientDictionary.ContainsKey(fileName))
                {
                    clientDictionary.Add(fileName, new List<ClientInfo>{(value)});
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

        // PUT: api/ClientInfos/5
        //[HttpPut("{fileName}")]
        //public void Put(string fileName, [FromBody] ClientInfo value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{fileName}")]
        public int Delete(string fileName, [FromBody] ClientInfo value)
        {
            try
            {
                if (clientDictionary.ContainsKey(fileName))
                {
                    List<ClientInfo> clientInformations = clientDictionary[fileName];
                    ClientInfo clientInformationToRemove = clientDictionary[fileName].Find(ci => ci.Equals(value));
                    clientInformations.Remove(clientInformationToRemove);
                    clientDictionary[fileName] = clientInformations;
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
