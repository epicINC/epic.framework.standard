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
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static long To(DateTime value)
        {
            return ((DateTimeOffset)value).ToUniversalTime().ToUnixTimeMilliseconds();
        }

        public static DateTime From(long value)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime.ToLocalTime();
        }
    }
}
