namespace URDashboardLibrary
{
    public interface IURDashboard
    {
        void Close();
        void Dispose();
        string Receive();
        void Send(string command);
        string SendReceive(string command);
    }
}