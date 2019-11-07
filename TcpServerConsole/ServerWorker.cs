using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;


namespace TcpServerConsole
{
    class ServerWorker
    {
        private string ipAddress;
        private int port;

        private ClientInfo serverInfo;

        private const string path = @"C:\Users\lll42\Desktop\Files";
        private const string URI = "http://localhost:5703/api/ClientInfos";

        private List<string> filesToShare = new List<string>();


        public void Start()
        {
            GenerateClientInfo();

            FindFilesToShare();

            RegistereFilesToRestService();

            RunServer();

            DeregistereFilesFromRestService();
        }

        private void RunServer()
        {
            TcpListener server = new TcpListener(IPAddress.Parse(ipAddress), port);
            server.Start();

            bool running = true;

            TcpClient socket = server.AcceptTcpClient();

            using (StreamReader reader = new StreamReader(socket.GetStream()))
            using (StreamWriter writer = new StreamWriter(socket.GetStream()))
            {
                string readerString = reader.ReadLine();

                writer.WriteLine("Sharing: " + readerString);
                writer.Flush();

                writer.WriteLine("Files: ");
                for (int i = 0; i < filesToShare.Count; i++)
                {
                    writer.WriteLine((i + 1) + " : " + filesToShare[i]);
                    writer.Flush();
                }
                writer.WriteLine("Do you wish to download other files? Y/N");
                readerString = reader.ReadLine();

                if (readerString?.ToLower() == "y")
                {
                    while (running)
                    {
                        writer.WriteLine("Files: ");
                        for (int i = 0; i < filesToShare.Count; i++)
                        {
                            writer.WriteLine((i + 1) + " : " + filesToShare[i]);
                            writer.Flush();
                        }

                        readerString = reader.ReadLine();

                        if (readerString?.ToLower() == "exit" || readerString?.ToLower() == "close" || readerString?.ToLower() == "stop")
                        {
                            running = false;
                        }
                        else
                        {
                            writer.WriteLine("Sending files...");
                            writer.Flush();
                        }
                    }
                }
            }
        }

        private void DeregistereFilesFromRestService()
        {
            using (HttpClient client = new HttpClient())
            {
                foreach (var filename in filesToShare)
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, URI + "/" + filename)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(serverInfo), Encoding.UTF8, "application/json")
                    };

                    client.SendAsync(request);
                }
            }
        }

        private void RegistereFilesToRestService()
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(serverInfo);
                
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                foreach (var filename in filesToShare)
                {
                    var httpResponseMessage = client.PostAsync(URI + "/" + filename, content).Result;
                    Console.WriteLine(httpResponseMessage.StatusCode);
                }
            }
        }

        private void GenerateClientInfo()
        {
            Console.WriteLine("Please insert designated port number...");
            if (!int.TryParse(Console.ReadLine(), out port))
            {
                GenerateClientInfo();
            }

            Console.WriteLine("Please insert designated ip address...");
            ipAddress = Console.ReadLine();

            try
            {
                serverInfo = new ClientInfo(port, ipAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                GenerateClientInfo();
            }
        }

        private void FindFilesToShare()
        {
            if (Directory.Exists(path))
            {
                foreach (string filename in Directory.GetFiles(path))
                {
                    filesToShare.Add(Path.GetFileName(filename));
                }
            }
        }
    }
}
