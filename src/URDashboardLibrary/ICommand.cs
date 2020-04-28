namespace URDashboardLibrary
{
    public static class ICommand
    {
        #region command strings
        private const string LOAD_PROGRAM = "load";
        private const string PLAY_PROGRAM = "play";
        private const string STOP_PROGRAM = "stop";
        private const string PAUSE_PROGRAM = "pause";
        private const string DISCONNECT = "quit";
        private const string SHUTDOWN = "shutdown";
        private const string IS_RUNNING = "running";
        private const string ROBOT_MODE = "robotmode";
        private const string GET_LOADED_PROGRAM = "get loaded program";
        private const string POPUP_MESSAGE = "popup";
        private const string CLOSE_POPUP = "close popup";
        private const string ADD_LOG_MESSAGE = "addToLog";
        private const string IS_PROGRAM_SAVED = "isProgramSaved";
        private const string PROGRAM_STATE = "programState";
        private const string POLYSCOPE_VERSION = "PolyscopeVersion";
        private const string SET_USER_ROLE = "setUserRole";
        private const string POWER_ON = "power on";
        private const string POWER_OFF = "power off";
        private const string RELEASE_BRAKE = "brake release";
        private const string UNLOCK_PROTECTIVE_STOP = "unlock protective stop";
        private const string CLOSE_SAFTY_POPUP = "close safety popup";
        private const string LOAD_INSTALLATION = "load installation";
        private const string RESTART_SAFETY = "restart safety";
        private const string SAFETY_STATUS = "safetystatus";
        private const string GET_SERIAL_NUMBER = "get serial number";
        private const string GET_ROBOT_MODEL = "get robot model";
        #endregion

        public static string LoadProgram(string programFile)
        {
            return $"{ LOAD_PROGRAM } { programFile }";
        }

        public static string PlayProgram()
        {
            return PLAY_PROGRAM;
        }

        public static string StopProgram()
        {
            return STOP_PROGRAM;
        }

        public static string PauseProgram()
        {
            return PAUSE_PROGRAM;
        }

        public static string Disconnect()
        {
            return DISCONNECT;
        }

        public static string Shutdown()
        {
            return SHUTDOWN;
        }

        public static string IsRunning()
        {
            return IS_RUNNING;
        }

        public static string RobotMode()
        {
            return ROBOT_MODE;
        }

        public static string GetLoadedProgram()
        {
            return GET_LOADED_PROGRAM;
        }

        public static string PopupMessage(string message)
        {
            return $"{ POPUP_MESSAGE } { message }";
        }

        public static string ClosePopup()
        {
            return CLOSE_POPUP;
        }

        public static string AddLogMessage(string message)
        {
            return $"{ ADD_LOG_MESSAGE } { message }";
        }

        public static string IsProgramSaved()
        {
            return IS_PROGRAM_SAVED;
        }

        public static string ProgramState()
        {
            return PROGRAM_STATE;
        }

        public static string PolyscopeVersion()
        {
            return POLYSCOPE_VERSION;
        }

        public static string SetUserRole(string role)
        {            
            return $"{ SET_USER_ROLE } { role }"; ;
        }

        public static string PowerOn()
        {
            return POWER_ON;
        }

        public static string PowerOff()
        {
            return POWER_OFF;
        }

        public static string ReleaseBrake()
        {
            return RELEASE_BRAKE;
        }

        public static string UnlockProtectiveStop()
        {
            return UNLOCK_PROTECTIVE_STOP;
        }

        public static string CloseSaftyPopup()
        {
            return CLOSE_SAFTY_POPUP;
        }

        public static string LoadInstallation(string installationFile)
        {
            return $"{ LOAD_INSTALLATION } { installationFile }";
        }

        public static string RestartSafty()
        {
            return RESTART_SAFETY;
        }

        public static string SaftyStatus()
        {
            return SAFETY_STATUS;
        }

        public static string GetSerialNumber()
        {
            return GET_SERIAL_NUMBER;
        }

        public static string GetRobotModel()
        {
            return GET_ROBOT_MODEL;
        }
    }
}
