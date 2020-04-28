using System;
using System.Net;
using System.Net.Sockets;
using Xunit;

namespace URSocketLibrary.Tests
{
    public class IURConnectionTests : IDisposable
    {
        public const string VALID_SERVER = "127.0.0.1";
        public const int PORT = 8444;
        public Socket socket { get; set; }
        public TcpListener server { get; set; }
        public const int Timeout = 500; // socket receive timeout 500 ms
     
        public IURConnectionTests()
        {
            IPAddress address = IPAddress.Parse(VALID_SERVER);

            // Start a server
            server = new TcpListener(address, PORT);
            server.Start();
        }

        public void Dispose()
        {
            server.Stop();
        }

        [Fact]
        public void Create_ValidIpAddressReturnsConnectedURSocket()
        {
            var urSocket = IURConnection.Create(VALID_SERVER, PORT);

            Assert.NotNull(urSocket);

            var expected = ConnectionState.CONNECTED;

            var actual = urSocket.State;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("111.111.111.111", 8444, SocketError.TimedOut)]
        [InlineData("111.111.111", 8444, SocketError.TimedOut)]
        [InlineData("127.0.0.1", 8888, SocketError.ConnectionRefused)]
        public void Create_WrongIpPortReturnsNull(string server, int port, SocketError expectedError)
        {
            try
            {
                var actual = IURConnection.Create(server, port);
                Assert.Null(actual);
            }
            catch (SocketException e)
            {
                Assert.Equal(expectedError, e.SocketErrorCode);
            }
            
        }
    }
}
