﻿using Autofac.Extras.Moq;
using Moq;
using System.Text;
using URSocketLibrary;
using Xunit;

namespace URSecondaryLibrary.Tests
{
    public class URSecondaryTests
    {
        [Fact]
        public void Ctor_NullURSocketShouldDisconnect()
        {
            var URSecondary = new URSecondary(null);

            var actual = URSecondary.IsConnected;

            Assert.False(actual);
        }

        [Theory]
        [InlineData(ConnectionState.CONNECTED, true)]
        [InlineData(ConnectionState.DISCONNECTED, false)]
        public void Ctor_ValidateURSocketState(ConnectionState urSocketState, bool URSecondaryState)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(urSocketState);

                var mockURSecondary = mock.Create<URSecondary>();

                var actual = mockURSecondary.IsConnected;

                Assert.Equal(URSecondaryState, actual);
            }
        }

        [Fact]
        public void Dispose_ShouldDisconnect()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.CONNECTED);

                var mockURSecondary = mock.Create<URSecondary>();

                mockURSecondary.Dispose();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Dispose(), Times.Exactly(1));

                var actual = mockURSecondary.IsConnected;

                Assert.False(actual);
            }
        }

        [Fact]
        public void Close_ShouldDisconnect()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.CONNECTED);

                var mockURSecondary = mock.Create<URSecondary>();

                mockURSecondary.Close();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Close(), Times.Exactly(1));

                var actual = mockURSecondary.IsConnected;

                Assert.False(actual);
            }
        }

        [Theory]
        [InlineData(ConnectionState.DISCONNECTED, 0)]
        [InlineData(ConnectionState.CONNECTED, 1)]
        public void Send_ValidateSendMethod(ConnectionState connState, int calledTimes)
        {
            using (var mock = AutoMock.GetLoose())
            {
                string command = "Command";
                byte[] bcommand = Encoding.ASCII.GetBytes(command + '\n');

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(connState);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Send(bcommand));

                var mockURSecondary = mock.Create<URSecondary>();


                mockURSecondary.Send(command);

                mock.Mock<IURSocket>()
                    .Verify(x => x.Send(bcommand), Times.Exactly(calledTimes));
            }
        }

        [Theory]
        [InlineData(ConnectionState.DISCONNECTED, 0, "")]
        [InlineData(ConnectionState.CONNECTED, 1, "Response")]
        public void Receive_ValidateReceiveMethod(ConnectionState connState, int calledTimes, string expected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                int bufferSize = 4096;
                string response = "Response";
                byte[] brespond = Encoding.ASCII.GetBytes(response);

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(connState);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Receive(bufferSize))
                    .Returns(brespond);

                var mockURSecondary = mock.Create<URSecondary>();

                var actual = mockURSecondary.Receive();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(calledTimes));

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void SendReceive_ShouldCallSendAndReceiveMethod()
        {
            using (var mock = AutoMock.GetLoose())
            {
                int bufferSize = 4096;
                string command = "test";
                byte[] sent = Encoding.ASCII.GetBytes(command + '\n');
                byte[] received = Encoding.ASCII.GetBytes(command);

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.CONNECTED);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Send(sent));

                mock.Mock<IURSocket>()
                    .Setup(x => x.Receive(bufferSize))
                    .Returns(received);

                var mockURSecondary = mock.Create<URSecondary>();

                var expected = command;

                var actual = mockURSecondary.SendReceive(command);

                mock.Mock<IURSocket>()
                    .Verify(x => x.Send(sent), Times.Exactly(1));

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(1));

                Assert.Equal(command, actual);

            }
        }

        [Fact]
        public void ReceiveBytes_ConnectedShouldReceive()
        {
            using (var mock = AutoMock.GetLoose())
            {
                int bufferSize = 4096;
                byte[] response = { 0x00, 0x01, 0x02 };
                
                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.CONNECTED);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Receive(bufferSize))
                    .Returns(response);

                var mockURSecondary = mock.Create<URSecondary>();

                var expected = response;

                var actual = mockURSecondary.ReceiveBytes();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(1));

                Assert.Equal(expected.Length, actual.Length);
            }
        }

        [Fact]
        public void ReceiveBytes_DisconnectedShouldNotReceive()
        {
            using (var mock = AutoMock.GetLoose())
            {
                int bufferSize = 4096;
                byte[] response = { 0x00, 0x01, 0x02 };

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.DISCONNECTED);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Receive(bufferSize))
                    .Returns(response);

                var mockURSecondary = mock.Create<URSecondary>();

                var actual = mockURSecondary.ReceiveBytes();

                byte[] expected = { };

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(0));

                Assert.Equal(expected.Length, actual.Length);
            }
        }

        [Fact]
        public void ReceiveBytes_ValidateReceiveBytesMethod()
        {
            using (var mock = AutoMock.GetLoose())
            {
                int bufferSize = 4096;
                byte[] response = { 0x00, 0x01, 0x02 };

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(ConnectionState.CONNECTED);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Receive(bufferSize))
                    .Returns(response);

                var mockURSecondary = mock.Create<URSecondary>();

                var expected = response;

                var actual = mockURSecondary.ReceiveBytes();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(1));

                Assert.Equal(expected.Length, actual.Length);

                for (int i = 0; i < expected.Length; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }
        }
    }
}
