using System;
using System.Text;

namespace URRTDELibrary
{
    public static class IFromBytes
    {
        public static uint ToUInt32(byte[] data)
        {
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        public static int ToInt32(byte[] data)
        {
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public static string ToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static double ToDouble(byte[] data)
        {
            Array.Reverse(data);
            return BitConverter.ToDouble(data, 0);
        }

        public static double[] To6Double(byte[] data)
        {
            double[] result = new double[6];

            if (data.Length == 48)
            {
                byte[] temp = new byte[8];
                for (int i = 0; i < 6; i++)
                {
                    Buffer.BlockCopy(data, i * 8, temp, 0, 8);
                    result[i] = ToDouble(temp);
                }
            }

            return result;
        }
    }
}
