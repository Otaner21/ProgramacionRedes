using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocol;

Socket serverSocket = new Socket(
    AddressFamily.InterNetwork, // IP v4
    SocketType.Stream,
    ProtocolType.Tcp);
IPEndPoint serverIpEndPoint = new IPEndPoint(
    IPAddress.Parse("127.0.0.1"),
    6000); //ip endpoint => par ip y puerto
serverSocket.Bind(serverIpEndPoint); // vinculo el socket con el endpoint

Console.WriteLine("Starting server...");
serverSocket.Listen(100); // pongo al server socket en modo servidor
Console.WriteLine("Waiting for clients...");

Socket clientSocket = serverSocket.Accept(); // El accept es bloqueante, espera hasta que llega una nueva conexión
NetworkDataHelper networkDataHelper = new NetworkDataHelper(clientSocket);
Console.WriteLine("Client connected");

Console.WriteLine("Waiting for client messages");
bool clientStopped = false;
while (!clientStopped)
{
    try
    {
        byte[] dataLength = networkDataHelper.Receive(Constants.FixedLength); // Recibo la parte fija de los datos
        byte[] data = networkDataHelper.Receive(BitConverter.ToInt32(dataLength)); // Recibo los datos (parte variable)
        string message = $"Message: {Encoding.UTF8.GetString(data)}";
        Console.WriteLine(message);
    }
    catch (SocketException)
    {
        Console.WriteLine("Client disconnected");
        clientStopped = true;
    }
}

Console.WriteLine("Press return to exit");