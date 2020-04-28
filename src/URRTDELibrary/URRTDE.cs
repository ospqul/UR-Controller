using System;
using URSocketLibrary;

namespace URRTDELibrary
{
    public class URRTDE : IDisposable, IURRTDE
    {
        private IURSocket _urSocket { get; set; }

        public URRTDE(IURSocket urSocket)
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

        public void Send(byte packageType, byte[] payload)
        {
            var package = IRTDEPackage.Pack(packageType, payload);
            _urSocket.Send(package);
        }

        public byte[] Receive(byte packageType)
        {
            int offset = 3; // 2 bytes of size and 1 byte of package type
            var buffer = _urSocket.Receive();
            byte[] bSize = new byte[2];
            Buffer.BlockCopy(buffer, 0, bSize, 0, 2);
            Array.Reverse(bSize);
            int size = BitConverter.ToUInt16(bSize, 0);
            if (buffer.Length >= offset)
            {
                byte[] data = new byte[size - offset];
                if (buffer[2] == packageType)
                {
                    Buffer.BlockCopy(buffer, offset, data, 0, size - offset);
                    return data;
                }
            }
            return new byte[0];
        }

        public byte[] SendReceive(byte packageType, byte[] payload)
        {
            Send(packageType, payload);
            return Receive(packageType);
        }
    }
}
