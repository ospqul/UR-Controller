using URSocketLibrary;

namespace URDashboardLibrary
{
    public static class IURDashboardConnection
    {
        public static IURDashboard Create(string server)
        {
            var urSocket = IURConnection.Create(server, 29999);
            IURDashboard urDashboard = new URDashboard(urSocket);
            return urDashboard;
        }
    }
}
