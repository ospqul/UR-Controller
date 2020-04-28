namespace URRTDELibrary
{
    public static class IPackageType
    {
        public const byte RTDE_REQUEST_PROTOCOL_VERSION = 86;        // ascii V
        public const byte RTDE_GET_URCONTROL_VERSION = 118;          // ascii v
        public const byte RTDE_TEXT_MESSAGE = 77;                    // ascii M
        public const byte RTDE_DATA_PACKAGE = 85;                    // ascii U
        public const byte RTDE_CONTROL_PACKAGE_SETUP_OUTPUTS = 79;   // ascii O
        public const byte RTDE_CONTROL_PACKAGE_SETUP_INPUTS = 73;    // ascii I
        public const byte RTDE_CONTROL_PACKAGE_START = 83;           // ascii S
        public const byte RTDE_CONTROL_PACKAGE_PAUSE = 80;           // ascii P
    }
}
