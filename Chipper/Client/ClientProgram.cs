using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocol;

Socket clientSocket = new Socket(
    AddressFamily.InterNetwork, // IP v4
    SocketType.Stream,
    ProtocolType.Tcp);
IPEndPoint clientIpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0); // poner el port en 0 busca el primer puerto disponible
clientSocket.Bind(clientIpEndPoint); // vínculo el socket con el endpoint
Console.WriteLine("Starting client...");
Console.WriteLine("Connecting to server...");
IPEndPoint serverIpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);
clientSocket.Connect(serverIpEndPoint);
NetworkDataHelper networkDataHelper = new NetworkDataHelper(clientSocket);
Console.WriteLine("Connected");
Console.WriteLine("Type a message and press return to send it");
bool stopped = false;

while (!stopped)
{
    string message = Console.ReadLine();
    if (message.Equals("Exit", StringComparison.InvariantCultureIgnoreCase))
    {
        stopped = true;
    }
    else
    {
        byte[] data = Encoding.UTF8.GetBytes(message); // Conversión de datos a bytes
        byte[] dataLength = BitConverter.GetBytes(data.Length); // Conversión del largo de los datos a bytes
        try
        {
            networkDataHelper.Send(dataLength);
            networkDataHelper.Send(data);
        }
        catch (SocketException)
        {
            Console.WriteLine("Connection with the server has been interrupted");
            break;
        }
    }
}

clientSocket.Shutdown(SocketShutdown.Both);
clientSocket.Close();        