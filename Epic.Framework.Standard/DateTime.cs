using Epic.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epic
{
    public class JDateTime
    {
        public static long Now()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
        }

        public static DateTime From(long value)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime;
        }
    }
}
