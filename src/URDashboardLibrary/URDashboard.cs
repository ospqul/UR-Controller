using System;
using URSocketLibrary;

namespace URDashboardLibrary
{
    public class URDashboard : IDisposable, IURDashboard
    {
        private IURSocket _urSocket { get; set; }
        public bool IsConnected { get; set; } = false;

        public URDashboard(IURSocket urSocket)
        {
            _urSocket = urSocket;

            if ((_urSocket != null) && (_urSocket.State == ConnectionState.CONNECTED))
            {                
                IsConnected = true;
            }
        }

        public void Dispose()
        {
            _urSocket.Dispose();
            IsConnected = false;
        }

        public void Close()
        {
            _urSocket.Close();
            IsConnected = false;
        }

        public void Send(string command)
        {
            if (IsConnected == true)
            {
                command += '\n';
                var package = IDashboardPackage.Pack(command);
                _urSocket.Send(package);
            }
        }

        public string Receive()
        {
            if (IsConnected == true)
            {
                var response = _urSocket.Receive();
                return IDashboardPackage.Unpack(response).TrimEnd('\n'); ;
            }
            return "";
        }

        public string SendReceive(string command)
        {
            Send(command);
            return Receive();
        }
    }
}
