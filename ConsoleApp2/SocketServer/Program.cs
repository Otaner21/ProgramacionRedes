using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Starting Server Application..");
            var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
            socketServer.Bind(localEndpoint);
            socketServer.Listen(1);
          /*  while (true)
            {
                var socketClient = socketServer.Accept();
                Console.WriteLine("Acepete un nuevo perdido de conexion");
            }*/
          var socketClient = socketServer.Accept();
            // Console.ReadLine();
        }
    }
}