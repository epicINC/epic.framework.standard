using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Tomorrow(this DateTime value)
        {
            return value.AddDays(1);
        }

        public static DateTime Yesterday(this DateTime value)
        {
            return value.AddDays(-1);
        }

    }
}
