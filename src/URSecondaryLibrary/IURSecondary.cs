namespace URSecondaryLibrary
{
    public interface IURSecondary
    {
        bool IsConnected { get; set; }

        void Close();
        void Dispose();
        string Receive();
        void Send(string command);
        string SendReceive(string command);
    }
}