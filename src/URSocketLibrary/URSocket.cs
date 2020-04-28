using System;
using System.Net.Sockets;

namespace URSocketLibrary
{
    public class URSocket : IDisposable, IURSocket
    {
        private Socket _socket { get; set; }
        public ConnectionState State { get; set; } = ConnectionState.DISCONNECTED;

        public URSocket(Socket socket)
        {
            _socket = socket;
            if (_socket != null)
            {
                State = ConnectionState.CONNECTED;
                _socket.ReceiveTimeout = 1000;
                _socket.SendTimeout = 1000;
            }
        }

        public void Dispose()
        {
            _socket.Dispose();
            State = ConnectionState.DISCONNECTED;
        }

        public void Close()
        {
            _socket.Close();
            State = ConnectionState.DISCONNECTED;
        }

        public void Send(byte[] package)
        {
            if (State == ConnectionState.CONNECTED)
            {
                _socket.Send(package, package.Length, 0);
            }
        }

        public byte[] Receive(int maxBufferSize = 4096)
        {
            if (State == ConnectionState.CONNECTED)
            {
                byte[] buffer = new byte[maxBufferSize];
                var bytes = _socket.Receive(buffer, buffer.Length, 0);
                if (bytes >= 0)
                {
                    byte[] response = new byte[bytes];
                    Buffer.BlockCopy(buffer, 0, response, 0, bytes);
                    return response;
                }
            }
            return new byte[0];
        }

    }
}
