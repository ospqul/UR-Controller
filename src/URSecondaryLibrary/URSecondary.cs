using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URSocketLibrary;

namespace URSecondaryLibrary
{
    public class URSecondary : IDisposable, IURSecondary
    {
        private IURSocket _urSocket { get; set; }
        public bool IsConnected { get; set; } = false;

        public URSecondary(IURSocket urSocket)
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
                var package = ISecondaryPackage.Pack(command);
                _urSocket.Send(package);
            }
        }

        public string Receive()
        {
            if (IsConnected == true)
            {
                var response = _urSocket.Receive();
                return ISecondaryPackage.Unpack(response).TrimEnd('\n');
            }
            return "";
        }

        public byte[] ReceiveBytes()
        {
            byte[] resp = { };
            if (IsConnected == true)
            {
                var response = _urSocket.Receive();
                return response;
            }
            return resp;
        }

        public string SendReceive(string command)
        {
            Send(command);
            return Receive();
        }
    }
}
