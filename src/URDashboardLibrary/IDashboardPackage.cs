using System.Text;

namespace URDashboardLibrary
{
    public static class IDashboardPackage
    {
        public static byte[] Pack(string command)
        {
            return Encoding.ASCII.GetBytes(command);
        }

        public static string Unpack(byte[] package)
        {
            return Encoding.ASCII.GetString(package);
        }
    }
}
