using URSocketLibrary;

namespace URSecondaryLibrary
{
    public static class IURSecondaryConnection
    {
        public static IURSecondary Create(string server)
        {
            var urSocket = IURConnection.Create(server, 30002);
            IURSecondary urSecond = new URSecondary(urSocket);
            return urSecond;
        }
    }
}
