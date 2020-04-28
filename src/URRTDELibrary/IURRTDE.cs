namespace URRTDELibrary
{
    public interface IURRTDE
    {
        void Close();
        void Dispose();
        byte[] Receive(byte packageType);
        void Send(byte packageType, byte[] payload);
        byte[] SendReceive(byte packageType, byte[] payload);
    }
}