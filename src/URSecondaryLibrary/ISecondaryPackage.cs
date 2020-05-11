using System.Text;

namespace URSecondaryLibrary
{
    public static class ISecondaryPackage
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
