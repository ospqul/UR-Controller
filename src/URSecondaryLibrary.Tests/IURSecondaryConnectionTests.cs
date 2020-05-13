using System;
using System.Net;
using System.Net.Sockets;
using Xunit;

namespace URSecondaryLibrary.Tests
{
    public class IURSecondaryConnectionTests : IDisposable
    {
        public TcpListener server { get; set; }
        public const string VALID_SERVER = "127.0.0.1";
        public const int PORT = 30002;

        public IURSecondaryConnectionTests()
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
        public void Create_ValidIpPortShouldReturnConnected()
        {
            var urSecondary = IURSecondaryConnection.Create(VALID_SERVER);

            Assert.NotNull(urSecondary);

            var actual = urSecondary.IsConnected;

            Assert.True(actual);
        }

        [Theory]
        [InlineData("111.111.111.111", SocketError.TimedOut)]
        public void Create_InvalidIpPortShouldReturnNotConnected(string server, SocketError error)
        {
            try
            {
                var urSecondary = IURSecondaryConnection.Create(server);

                Assert.NotNull(urSecondary);

                var actual = urSecondary.IsConnected;

                Assert.False(actual);
            }
            catch (SocketException e)
            {
                Assert.Equal(error, e.SocketErrorCode);
            }
        }
    }
}
