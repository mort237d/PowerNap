using System;

namespace TcpClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientWorker cw = new ClientWorker();
            cw.Start();

            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
