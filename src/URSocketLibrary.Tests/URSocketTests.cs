using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xunit;

namespace URSocketLibrary.Tests
{
    public class URSocketTests : IDisposable
    {
        public URSocket urSoket { get; set; }

        public const string SERVER = "127.0.0.1";
        public const int PORT = 8333;
        public Socket socket { get; set; }
        public TcpListener server { get; set; }
        public const int Timeout = 500; // socket receive timeout 500 ms

        public URSocketTests()
        {
            IPAddress address = IPAddress.Parse(SERVER);

            // Start a server
            server = new TcpListener(address, PORT);
            server.Start();
            
            IPEndPoint ipe = new IPEndPoint(address, PORT);
            socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipe);
        }

        public void Dispose()
        {
            server.Stop();
        }

        [Fact]
        public void Ctor_NullSocketShouldDisconnect()
        {
            urSoket = new URSocket(null);

            var expected = ConnectionState.DISCONNECTED;

            var actual = urSoket.State;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Ctor_ValidSocketShouldConnect()
        {
            urSoket = new URSocket(socket);

            var expected = ConnectionState.CONNECTED;

            var actual = urSoket.State;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dispose_ShouldDisconnected()
        {
            urSoket = new URSocket(socket);

            var expected = ConnectionState.DISCONNECTED;

            urSoket.Dispose();

            var actual = urSoket.State;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Close_ShouldDisconnected()
        {
            urSoket = new URSocket(socket);

            var expected = ConnectionState.DISCONNECTED;

            urSoket.Close();

            var actual = urSoket.State;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Send_BytesSentShouldBeEqualToBytesReceived()
        {
            urSoket = new URSocket(socket);

            var listener = server.AcceptSocket();
            listener.ReceiveTimeout = Timeout;

            string testString = "Test";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);

            var expected = bytes;

            urSoket.Send(bytes);

            byte[] buffer = new byte[100];
            var received = listener.Receive(buffer);
            byte[] actual = new byte[received];
            Buffer.BlockCopy(buffer, 0, actual, 0, received);

            Assert.Equal(expected.Length, actual.Length);
            for (int i=0; i<expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void Send_ShouldNotSendBytesWhenDisconnected()
        {
            urSoket = new URSocket(socket);
            urSoket.State = ConnectionState.DISCONNECTED;

            var listener = server.AcceptSocket();
            listener.ReceiveTimeout = Timeout;

            string testString = "Test";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);

            urSoket.Send(bytes);

            byte[] buffer = new byte[100];

            try
            {
                int expected = 0;
                var actual = listener.Receive(buffer);
                Assert.Equal(expected, actual);
            }
            catch (SocketException e)
            {
                // Actual: Socket exception code
                // Expected: TimeOut Exception
                Assert.Equal(SocketError.TimedOut, e.SocketErrorCode);
            }
        }

        [Fact]
        public void Receive_ShouldNotReceiveWhenDisconnected()
        {
            urSoket = new URSocket(socket);
            urSoket.State = ConnectionState.DISCONNECTED;

            var listener = server.AcceptSocket();
            listener.ReceiveTimeout = Timeout;
            
            string testString = "Test";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);

            listener.Send(bytes);
            
            try
            {
                var resp = urSoket.Receive();
                Assert.Empty(resp);
            }
            catch (SocketException e)
            {
                Assert.Equal(SocketError.TimedOut, e.SocketErrorCode);
            }
        }

        [Fact]
        public void Receive_ShouldReceiveSameBytes()
        {
            urSoket = new URSocket(socket);

            var listener = server.AcceptSocket();
            listener.ReceiveTimeout = Timeout;

            string testString = "Test";
            byte[] expected = Encoding.ASCII.GetBytes(testString);

            listener.Send(expected);

            var actual = urSoket.Receive();
            
            Assert.Equal(expected.Length, actual.Length);

            for(int i=0; i<expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }            
        }
    }
}
