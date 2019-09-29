using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Hardware.Extensions
{

    internal static class CastHelper
    {
        public static T? Cast<T>(this object value) where T : struct
        {
            if (value == null) return default;
            return (T)value;
        }

        public static T TryCast<T>(this object value) where T : class
        {
            if (value == null) return default;
            return value as T;
        }

        public static K[] TryCast<T, K>(this object value) where K : struct
        {
            var result = TryCast<T[]>(value);
            if (result == null) return default;
            return result.Select(e => CastHelper.Cast<K>(e).Value).ToArray();

        }
    }
}
