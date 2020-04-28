namespace URSocketLibrary
{
    public interface IURSocket
    {
        ConnectionState State { get; set; }

        void Close();
        void Dispose();
        byte[] Receive(int maxBufferSize = 4096);
        void Send(byte[] package);
    }
}