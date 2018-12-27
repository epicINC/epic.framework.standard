using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Security.Cryptography
{

    /// <summary>
    /// 
    /// </summary>
    /// <ref>
    /// https://github.com/xxtea/xxtea-dotnet/blob/master/XXTEA/XXTEA.cs
    /// https://www.cnblogs.com/linzheng/archive/2011/09/14/2176767.html
    /// https://github.com/beichenzhizuoshi/XXTEADecrypt/blob/65816df84cd43aab66f6ea8b917b9bdaa57962ee/XXTEADecrypt/XXTEAHelp.cs
    /// c++
    /// http://www.cnblogs.com/chevin/p/5681228.html
    /// https://www.cnblogs.com/huhu0013/p/3334890.html
    /// </ref>
    public static class XXTEA
    {
        // Methods
        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            if (data.Length == 0) return data;
            return ToByteArray(Decrypt(ToUInt32Array(data, false), ToUInt32Array(key, false)), true);
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

        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            if (data.Length == 0)
                return data;
            return ToByteArray(Encrypt(ToUInt32Array(data, true), ToUInt32Array(key, false)), false);
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

        private static byte[] ToByteArray(uint[] data, bool includeLength)
        {
            int n = includeLength ? ((int)data[data.Length - 1]) : (data.Length << 2);
            byte[] Result = new byte[n];
            for (int i = 0; i < n; i++)
            {
                Result[i] = (byte)(data[i >> 2] >> ((i & 3) << 3));
            }
            return Result;
        }

        private static uint[] ToUInt32Array(byte[] data, bool includeLength)
        {
            uint[] Result;
            int n = ((data.Length & 3) == 0) ? (data.Length >> 2) : ((data.Length >> 2) + 1);
            if (includeLength)
            {
                Result = new uint[n + 1];
                Result[n] = (uint)data.Length;
            }
            else
            {
                Result = new uint[n];
            }
            n = data.Length;
            for (int i = 0; i < n; i++)
            {
                Result[i >> 2] |= (uint)(data[i] << ((i & 3) << 3));
            }
            return Result;
        }
    }


}
