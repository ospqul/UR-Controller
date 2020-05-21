namespace URSecondaryLibrary
{
    public interface IURSecondary
    {
        bool IsConnected { get; set; }

        void Close();
        void Dispose();
        string Receive();
        byte[] ReceiveBytes();
        void Send(string command);
        string SendReceive(string command);
    }
}