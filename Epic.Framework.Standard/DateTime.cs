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
            return DateTimeExtensions.ToUnixTimeMilliseconds(DateTime.Now);
        }
    }
}
