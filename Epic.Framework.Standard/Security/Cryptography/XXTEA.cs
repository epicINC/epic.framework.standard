using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Security.Cryptography
{

    public static class XXTEA
    {
        // Methods
        public static byte[] Decrypt(byte[] key, byte[] Data)
        {
            if (Data.Length == 0)
                return Data;
            return ToByteArray(Decrypt(ToUInt32Array(Data, false), ToUInt32Array(key, false)), true);
        }

        public static uint[] Decrypt(uint[] v, uint[] k)
        {
            int n = v.Length - 1;
            if (n >= 1)
            {
                if (k.Length < 4)
                {
                    uint[] Key = new uint[4];
                    k.CopyTo(Key, 0);
                    k = Key;
                }
                uint z = v[n];
                uint y = v[0];
                uint delta = 0x9e3779b9;
                int q = 6 + (0x34 / (n + 1));
                for (uint sum = (uint)(q * delta); sum != 0; sum -= delta)
                {
                    uint e = (sum >> 2) & 3;
                    int p = n;
                    while (p > 0)
                    {
                        z = v[p - 1];
                        y = v[p] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(int)((IntPtr)((p & 3) ^ e))] ^ z));
                        p--;
                    }
                    z = v[n];
                    y = v[0] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(int)((IntPtr)((p & 3) ^ e))] ^ z));
                }
            }
            return v;
        }

        public static byte[] Encrypt(byte[] Key, byte[] Data)
        {
            if (Data.Length == 0)
                return Data;
            return ToByteArray(Encrypt(ToUInt32Array(Data, true), ToUInt32Array(Key, false)), false);
        }

        public static uint[] Encrypt(uint[] v, uint[] k)
        {
            int n = v.Length - 1;
            if (n >= 1)
            {
                if (k.Length < 4)
                {
                    uint[] Key = new uint[4];
                    k.CopyTo(Key, 0);
                    k = Key;
                }
                uint z = v[n];
                uint y = v[0];
                uint delta = 0x9e3779b9;
                uint sum = 0;
                int q = 6 + (0x34 / (n + 1));
                while (q-- > 0)
                {
                    sum += delta;
                    uint e = (sum >> 2) & 3;
                    int p = 0;
                    while (p < n)
                    {
                        y = v[p + 1];
                        z = v[p] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(int)((IntPtr)((p & 3) ^ e))] ^ z));
                        p++;
                    }
                    y = v[0];
                    z = v[n] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(int)((IntPtr)((p & 3) ^ e))] ^ z));
                }
            }
            return v;
        }

        private static byte[] ToByteArray(uint[] Data, bool IncludeLength)
        {
            int n = IncludeLength ? ((int)Data[Data.Length - 1]) : (Data.Length << 2);
            byte[] Result = new byte[n];
            for (int i = 0; i < n; i++)
            {
                Result[i] = (byte)(Data[i >> 2] >> ((i & 3) << 3));
            }
            return Result;
        }

        private static uint[] ToUInt32Array(byte[] Data, bool IncludeLength)
        {
            uint[] Result;
            int n = ((Data.Length & 3) == 0) ? (Data.Length >> 2) : ((Data.Length >> 2) + 1);
            if (IncludeLength)
            {
                Result = new uint[n + 1];
                Result[n] = (uint)Data.Length;
            }
            else
            {
                Result = new uint[n];
            }
            n = Data.Length;
            for (int i = 0; i < n; i++)
            {
                Result[i >> 2] |= (uint)(Data[i] << ((i & 3) << 3));
            }
            return Result;
        }
    }


}
