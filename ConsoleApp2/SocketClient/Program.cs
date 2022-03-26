using System;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Startiing CLient Aplication...");
            var socketClient= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15000);
            var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
            socketClient.Bind(localEndpoint);
            socketClient.Connect(remoteEndpoint);
            Console.WriteLine("Connected to server!!");
            Console.ReadLine();
        }
    }
}