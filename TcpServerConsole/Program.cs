using System;

namespace TcpServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerWorker cw = new ServerWorker();
            cw.Start();

            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
