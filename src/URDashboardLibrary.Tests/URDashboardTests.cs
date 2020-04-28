using Autofac.Extras.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URSocketLibrary;
using Xunit;

namespace URDashboardLibrary.Tests
{
    public class URDashboardTests
    {
        [Fact]
        public void Ctor_NullURSocketShouldDisconnect()
        {
            var urDashboard = new URDashboard(null);

            var actual = urDashboard.IsConnected;

            Assert.False(actual);
        }

        [Theory]
        [InlineData(ConnectionState.CONNECTED, true)]
        [InlineData(ConnectionState.DISCONNECTED, false)]
        public void Ctor_ValidateURSocketState(ConnectionState urSocketState, bool urDashboardState)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(urSocketState);

                var mockURDashboard = mock.Create<URDashboard>();

                var actual = mockURDashboard.IsConnected;

                Assert.Equal(urDashboardState, actual);                
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

                var mockURDashboard = mock.Create<URDashboard>();

                mockURDashboard.Dispose();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Dispose(), Times.Exactly(1));

                var actual = mockURDashboard.IsConnected;

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

                var mockURDashboard = mock.Create<URDashboard>();

                mockURDashboard.Close();

                mock.Mock<IURSocket>()
                    .Verify(x => x.Close(), Times.Exactly(1));

                var actual = mockURDashboard.IsConnected;

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
                byte[] bcommand = Encoding.ASCII.GetBytes(command+'\n');

                mock.Mock<IURSocket>()
                    .Setup(x => x.State)
                    .Returns(connState);

                mock.Mock<IURSocket>()
                    .Setup(x => x.Send(bcommand));

                var mockURDashboard = mock.Create<URDashboard>();


                mockURDashboard.Send(command);

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

                var mockURDashboard = mock.Create<URDashboard>();

                var actual = mockURDashboard.Receive();

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

                var mockURDashboard = mock.Create<URDashboard>();

                var expected = command;

                var actual = mockURDashboard.SendReceive(command);

                mock.Mock<IURSocket>()
                    .Verify(x => x.Send(sent), Times.Exactly(1));

                mock.Mock<IURSocket>()
                    .Verify(x => x.Receive(bufferSize), Times.Exactly(1));

                Assert.Equal(command, actual);

            }
        }
    }
}
