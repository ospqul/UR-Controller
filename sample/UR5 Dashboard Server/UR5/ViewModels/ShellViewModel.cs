using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using URDashboardLibrary;
using URSocketLibrary;

namespace UR5.ViewModels
{
    class ShellViewModel : Screen
    {
        public IURDashboard urDashboard { get; set; }

        public double File { get; set; }

        private string _ipAddress;
        private string _serialNumber;
        private string _logging;
        private string _status;

        private string _fileName;

        private string _runningStatus;
        private string _state;
        private string _saveState;
        private string _safetyStatus;
        private string _robotMode;
        private string _programName;

        public ShellViewModel()
        {
            IpAddress = "192.168.245.129";
            Logging += $"IP Address is: { IpAddress }" + Environment.NewLine;
        }

        public void Connect()
        {
            // Connect to UR
            urDashboard = IURDashboardConnection.Create(IpAddress);

            // Read connection message
            var resp = urDashboard.Receive();
            Logging += resp + Environment.NewLine;

            // All commands are under ICommand class
            var cmd = ICommand.GetSerialNumber();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            resp = urDashboard.SendReceive(cmd);
            SerialNumber = resp;
            Logging += $"[Receive]: { resp }" + Environment.NewLine;

            polyscopeVersion();
            RobotModel();
        }

        public void Load()
        {
            var cmd = ICommand.LoadProgram(FileName);
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
            //if ( = FileName)
            //{
            //    PopUpMessage("Program is Loaded");
            //}
            //else
            //{
            //    PopUpMessage("Program is not Loaded");
            //}
            //GetFileName();
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                NotifyOfPropertyChange(() => IpAddress);
            }
        }
        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                _serialNumber = value;
                NotifyOfPropertyChange(() => SerialNumber);
            }
        }
        public void polyscopeVersion()
        {
            var cmd = ICommand.PolyscopeVersion();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void RobotModel()
        {
            var cmd = ICommand.GetRobotModel();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }

        public string Logging
        {
            get { return _logging; }
            set
            {
                _logging = value;
                NotifyOfPropertyChange(() => Logging);
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        //Robot Setting 
        public void Play()
        {
            var cmd = ICommand.PlayProgram();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void Pause()
        {
            var cmd = ICommand.PauseProgram();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void Stop()
        {
            var cmd = ICommand.StopProgram();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }

        //Robot Arm Settings
        public void PowerOnRobotArm()
        {
            var cmd = ICommand.PowerOn();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void PowerOffRobotArm()
        {
            var cmd = ICommand.PowerOff();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void ReleaseRobotArmBrake()
        {
            var cmd = ICommand.ReleaseBreak();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }

        //Robot Safety Setting 
        public void UnlockProtectiveStop()
        {
            var cmd = ICommand.UnlockProtectiveStop();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
            //PopUpMessage();
        }
        public void RestartRobotSafety()
        {
            var cmd = ICommand.RestartSafty();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
            //PopUpMessage();
        }
        public void CloseSafetyPopup()
        {
            var cmd = ICommand.CloseSaftyPopup();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }

        //Program Status Settings
        public void ProgramRunningStatus()
        {
            var cmd = ICommand.IsRunning();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            RunningStatus = resp.Substring(17);
            //RunningStatus = resp;
        }
        public string RunningStatus
        {
            get { return _runningStatus; }
            set
            {
                _runningStatus = value;
                NotifyOfPropertyChange(() => RunningStatus);
            }
        }

        public void ProgramState()
        {
            var cmd = ICommand.ProgramState();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            State = resp.Substring(0, 7);
        }//(PLAY,PAUSE,STOP)
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyOfPropertyChange(() => State);
            }
        }

        public void ProgramSaveStatus()
        {
            var cmd = ICommand.IsProgramSaved();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            SaveState = resp.Substring(0, 5);
        }
        public string SaveState
        {
            get { return _saveState; }
            set
            {
                _saveState = value;
                NotifyOfPropertyChange(() => SaveState);
            }
        }

        public void SafetyModeStatus()
        {
            var cmd = ICommand.SaftyStatus();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            SafetyStatus = resp.Substring(14);
        }
        public string SafetyStatus
        {
            get { return _safetyStatus; }
            set
            {
                _safetyStatus = value;
                NotifyOfPropertyChange(() => SafetyStatus);
            }
        }

        public void ProgramDetail()
        {
            var cmd = ICommand.RobotMode();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            RobotMode = resp.Substring(11);
        }
        public string RobotMode
        {
            get { return _robotMode; }
            set
            {
                _robotMode = value;
                NotifyOfPropertyChange(() => RobotMode);
            }
        }

        public void GetFileName()
        {
            var cmd = ICommand.GetLoadedProgram();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            //Logging += $"[Receive]: { resp }" + Environment.NewLine;
            string response = "No program loaded";
            if (resp != response)
            {
                ProgramName = resp.Substring(16);
            }
            else
            {
                ProgramName = response;
            }
        }
        public string ProgramName
        {
            get { return _programName; }
            set
            {
                _programName = value;
                NotifyOfPropertyChange(() => ProgramName);
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                NotifyOfPropertyChange(() => FileName);
            }
        }


        public void GetProgramDetails()
        {
            GetFileName();
            ProgramRunningStatus();
            ProgramState();
            ProgramSaveStatus();
            SafetyModeStatus();
            ProgramDetail();
        }

        //End program
        public void DisconnectProgram()
        {
            var cmd = ICommand.Disconnect();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void ShutDownProgram()
        {
            var cmd = ICommand.Shutdown();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void EndProgram()
        {
            Stop();
            PowerOffRobotArm();
            DisconnectProgram();
            ShutDownProgram();
        }

        //Pop-Up Message
        public void PopUpMessage(string problem)
        {
            var cmd = ICommand.PopupMessage(problem);
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }
        public void ClosePopUpMessage()
        {
            var cmd = ICommand.ClosePopup();
            Logging += $"[Send]: { cmd }" + Environment.NewLine;

            var resp = urDashboard.SendReceive(cmd);
            Logging += $"[Receive]: { resp }" + Environment.NewLine;
        }

    }
}
