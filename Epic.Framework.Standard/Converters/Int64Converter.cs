using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Converters
{
    public static class Int64Converter
    {
        public static unsafe byte[] GetBytes(long value)
        {
            byte[] result = new byte[8];
            fixed (byte* point = result)
            {
                *((long*)point) = value;
            }
            return result;
        }
    }
}
