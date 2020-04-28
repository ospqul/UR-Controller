using System.Net;
using System.Net.Sockets;

namespace URSocketLibrary
{
    public static class IURConnection
    {
        public static IURSocket Create(string server, int port)
        {
            IURSocket urSocket = null;

            IPAddress address = IPAddress.Parse(server);
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            socket.Connect(ipe);

            if (socket.Connected)
            {
                urSocket = new URSocket(socket);
            }

            return urSocket;
        }
    }
}
