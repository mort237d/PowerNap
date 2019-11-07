using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace TcpClientConsole
{
    class ClientWorker
    {
        private const string URI = "http://localhost:5703/api/ClientInfos";

        public void Start()
        {
            Console.WriteLine("Please enter file you are searching for:");
            string fileName = "/" + Console.ReadLine();

            if (SearchForPeers(fileName, out List<ClientInfo> clients))
            {
                Random rnd = new Random();

                ClientInfo hostInfo = clients[rnd.Next(clients.Count)];

                TcpClient host = new TcpClient(hostInfo.ClientIpAddress, hostInfo.PortNumber);

                using (StreamReader sr = new StreamReader(host.GetStream()))
                using (StreamWriter sw = new StreamWriter(host.GetStream()))
                {

                    sw.WriteLine(fileName.Substring(1));
                    sw.Flush();

                    string readerString = sr.ReadLine();
                    Console.WriteLine(readerString);
                }
            }
            else
            {
                Console.WriteLine("Could not find any files with the name: " + fileName.Substring(1));
                Start();
            }
        }


        private bool SearchForPeers(string fileName, out List<ClientInfo> result)
        {
            result = new List<ClientInfo>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Task<string> resTask = client.GetStringAsync(URI + fileName);
                    String jsonStr = resTask.Result;

                    result = JsonConvert.DeserializeObject<List<ClientInfo>>(jsonStr);

                    if (result.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}
