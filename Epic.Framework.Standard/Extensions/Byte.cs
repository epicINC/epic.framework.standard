using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHex(this byte[] value)
        {
            return Converters.HexString.Encode(value);
        }

        public static byte[] FromHex(this string value)
        {
            return Converters.HexString.Decode(value);
        }

    }
}
