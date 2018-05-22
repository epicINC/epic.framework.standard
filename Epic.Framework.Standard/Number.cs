using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct ByteToInt
    {
        [FieldOffset(0)]
        public byte[] Original;


        [FieldOffset(0)]
        public int Value;
    }


    public class JNumber
    {

        public static IEnumerable<int> Range(int start, int count)
        {
            return Enumerable.Range(start, count);
        }

        public static IEnumerable<int> Range(int count)
        {
            return Range(1, count);
        }




        public static double TruePercent()
        {
            using (var rng = System.Security.Cryptography.RNGCryptoServiceProvider.Create())
            {
                var result = new byte[1];
                rng.GetBytes(result);
                return (double)result[0] / 25500;
            }
        }

        public static int TrueRandom()
        {
            using (var rng = System.Security.Cryptography.RNGCryptoServiceProvider.Create())
            {
                var result = new ByteToInt();
                result.Original = new byte[4];
                rng.GetBytes(result.Original);
                return result.Value;
            }
        }
    }
}
