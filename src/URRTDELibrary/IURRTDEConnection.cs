using URSocketLibrary;

namespace URRTDELibrary
{
    public class IURRTDEConnection
    {
        public static IURRTDE Create(string server)
        {
            var urSocket = IURConnection.Create(server, 30004);
            IURRTDE urRTDE = new URRTDE(urSocket);
            return urRTDE;
        }
    }
}
