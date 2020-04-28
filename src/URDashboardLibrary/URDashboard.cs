using System;
using URSocketLibrary;

namespace URDashboardLibrary
{
    public class URDashboard : IDisposable, IURDashboard
    {
        private IURSocket _urSocket { get; set; }

        public URDashboard(IURSocket urSocket)
        {
            if (urSocket != null)
            {
                _urSocket = urSocket;
            }
        }

        public void Dispose()
        {
            _urSocket.Dispose();
        }

        public void Close()
        {
            _urSocket.Close();
        }

        public void Send(string command)
        {
            command += '\n';
            var package = IDashboardPackage.Pack(command);
            _urSocket.Send(package);
        }

        public string Receive()
        {
            var response = _urSocket.Receive();
            return IDashboardPackage.Unpack(response).TrimEnd('\n'); ;
        }

        public string SendReceive(string command)
        {
            Send(command);
            return Receive();
        }
    }
}
