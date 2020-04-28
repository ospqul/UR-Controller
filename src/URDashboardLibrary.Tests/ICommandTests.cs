using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace URDashboardLibrary.Tests
{
    public class ICommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(".urp")]
        [InlineData("program.urp")]
        public void LoadProgram_ShouldReturnCorrectCommandString(string programFile)
        {
            var expected = $"load { programFile }";

            var actual = ICommand.LoadProgram(programFile);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlayProgram_ShouldReturnCorrectCommandString()
        {
            var expected = "play";

            var actual = ICommand.PlayProgram();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StopProgram_ShouldReturnCorrectCommandString()
        {
            var expected = "stop";

            var actual = ICommand.StopProgram();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PauseProgram_ShouldReturnCorrectCommandString()
        {
            var expected = "pause";

            var actual = ICommand.PauseProgram();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Disconnect_ShouldReturnCorrectCommandString()
        {
            var expected = "quit";

            var actual = ICommand.Disconnect();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Shutdown_ShouldReturnCorrectCommandString()
        {
            var expected = "shutdown";

            var actual = ICommand.Shutdown();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsRunning_ShouldReturnCorrectCommandString()
        {
            var expected = "running";

            var actual = ICommand.IsRunning();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RobotMode_ShouldReturnCorrectCommandString()
        {
            var expected = "robotmode";

            var actual = ICommand.RobotMode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetLoadedProgram_ShouldReturnCorrectCommandString()
        {
            var expected = "get loaded program";

            var actual = ICommand.GetLoadedProgram();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Popup message")]
        [InlineData("Popup message\n")]
        public void PopupMessage_ShouldReturnCorrectCommandString(string message)
        {
            var expected = $"popup { message }";

            var actual = ICommand.PopupMessage(message);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ClosePopup_ShouldReturnCorrectCommandString()
        {
            var expected = "close popup";

            var actual = ICommand.ClosePopup();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Logging message")]
        [InlineData("Logging message\n")]
        public void AddLogMessage_ShouldReturnCorrectCommandString(string message)
        {
            var expected = $"addToLog { message }";

            var actual = ICommand.AddLogMessage(message);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsProgramSaved_ShouldReturnCorrectCommandString()
        {
            var expected = "isProgramSaved";

            var actual = ICommand.IsProgramSaved();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProgramState_ShouldReturnCorrectCommandString()
        {
            var expected = "programState";

            var actual = ICommand.ProgramState();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PolyscopeVersion_ShouldReturnCorrectCommandString()
        {
            var expected = "PolyscopeVersion";

            var actual = ICommand.PolyscopeVersion();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("programmer")]
        [InlineData("operator")]
        [InlineData("none")]
        [InlineData("locked")]
        [InlineData("restricted")]
        public void SetUserRole_ShouldReturnCorrectCommandString(string role)
        {
            var expected = $"setUserRole { role }";

            var actual = ICommand.SetUserRole(role);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PowerOn_ShouldReturnCorrectCommandString()
        {
            var expected = "power on";

            var actual = ICommand.PowerOn();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PowerOff_ShouldReturnCorrectCommandString()
        {
            var expected = "power off";

            var actual = ICommand.PowerOff();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReleaseBrake_ShouldReturnCorrectCommandString()
        {
            var expected = "brake release";

            var actual = ICommand.ReleaseBrake();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnlockProtectiveStop_ShouldReturnCorrectCommandString()
        {
            var expected = "unlock protective stop";

            var actual = ICommand.UnlockProtectiveStop();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CloseSaftyPopup_ShouldReturnCorrectCommandString()
        {
            var expected = "close safety popup";

            var actual = ICommand.CloseSaftyPopup();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("default.installation")]
        [InlineData("program.installation\n")]
        public void LoadInstallation_ShouldReturnCorrectCommandString(string installationFile)
        {
            var expected = $"load installation { installationFile }";

            var actual = ICommand.LoadInstallation(installationFile);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RestartSafty_ShouldReturnCorrectCommandString()
        {
            var expected = "restart safety";

            var actual = ICommand.RestartSafty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SaftyStatus_ShouldReturnCorrectCommandString()
        {
            var expected = "safetystatus";

            var actual = ICommand.SaftyStatus();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSerialNumber_ShouldReturnCorrectCommandString()
        {
            var expected = "get serial number";

            var actual = ICommand.GetSerialNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRobotModel_ShouldReturnCorrectCommandString()
        {
            var expected = "get robot model";

            var actual = ICommand.GetRobotModel();

            Assert.Equal(expected, actual);
        }
    }
}
