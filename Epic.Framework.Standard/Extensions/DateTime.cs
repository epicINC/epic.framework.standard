using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimeMilliseconds(this DateTime value)
        {
            return ((DateTimeOffset)value).ToUnixTimeMilliseconds();
        }
    }
}
