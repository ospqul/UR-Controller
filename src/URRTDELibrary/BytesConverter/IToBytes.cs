using System;
using System.Text;

namespace URRTDELibrary
{
    public static class IToBytes
    {
        public static byte[] FromUshort(ushort obj, bool littleEndia = false)
        {
            byte[] result = BitConverter.GetBytes(obj);

            // LittleEndian to BigEndian
            if (littleEndia == false)
            {
                Array.Reverse(result);
            }

            return result;
        }

        public static byte[] FromDouble(double obj, bool littleEndia = false)
        {
            byte[] result = BitConverter.GetBytes(obj);

            // LittleEndian to BigEndian
            if (littleEndia == false)
            {
                Array.Reverse(result);
            }

            return result;
        }

        public static byte[] FromString(string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
    }
}
