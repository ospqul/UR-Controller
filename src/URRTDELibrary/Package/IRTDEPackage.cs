using System;

namespace URRTDELibrary
{
    public static class IRTDEPackage
    {
        public static byte[] Pack(byte packagetType, byte[] payload)
        {
            byte[] type = new byte[1];
            type[0] = packagetType;

            ushort size = Convert.ToUInt16(2 + type.Length + payload.Length);
            byte[] package = new byte[size];

            byte[] bSize = IToBytes.FromUshort(size);
            Buffer.BlockCopy(bSize, 0, package, 0, bSize.Length);
            Buffer.BlockCopy(type, 0, package, bSize.Length, type.Length);

            if (payload.Length > 0)
            {
                Buffer.BlockCopy(payload, 0, package, bSize.Length + type.Length, payload.Length);
            }

            return package;
        }
    }
}
