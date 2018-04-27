using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Components;
using System.Globalization;
using Epic.Extensions;

namespace Epic.Converters
{
    public static partial class StringConverter
    {
        public static string[] AsArray(string value)
        {
            return AsArray(value, ',');
        }

        public static string[] AsArray(string value, StringSplitOptions options)
        {
            return AsArray(value, new char[] { ',' }, options);
        }

        public static string[] AsArray(string value, params char[] separator)
        {
            if (String.IsNullOrEmpty(value)) return new string[] { };
            return value.Split(separator);
        }
        public static string[] AsArray(string value, char[] separator, StringSplitOptions options)
        {
            if (String.IsNullOrEmpty(value)) return new string[]{};
            return value.Split(separator, options);
        }

        public static string[] AsArray(string value, string[] separator, StringSplitOptions options)
        {
            if (String.IsNullOrEmpty(value)) return new string[] { };
            return value.Split(separator, options);
        }


        public static bool AsBool(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return false;
            value = value.Trim();
            if (value.Length == 1)
            {
                if (value[0] == '1') return true;
                if (value[0] == '0') return false;
            }
            if (value.IgnoreCaseEquals("true")) return true;
            if (value.IgnoreCaseEquals("false")) return false;

            return CommonConverter.Converter<string, bool>(value, Boolean.TryParse);
        }

        public static byte AsByte(string value, byte defaultValue = 0)
        {
            return CommonConverter.Converter<string, byte>(value, Byte.TryParse);
        }


        public static ushort AsUInt16(string value)
        {
            return CommonConverter.Converter<string, ushort>(value, UInt16.TryParse);
        }

        public static short AsInt16(string value)
        {
            return CommonConverter.Converter<string, short>(value, Int16.TryParse);
        }


        public static uint AsUInt32(string value)
        {
            return CommonConverter.Converter<string, uint>(value, UInt32.TryParse);
        }

        public static int AsInt32(string value)
        {
            return CommonConverter.Converter<string, int>(value, Int32.TryParse);
        }


        public static ulong AsUInt64(string value)
        {
            return CommonConverter.Converter<string, ulong>(value, UInt64.TryParse);
        }

        public static long AsInt64(string value)
        {
            return CommonConverter.Converter<string, long>(value, Int64.TryParse);
        }


        public static double AsDouble(string value)
        {
            return CommonConverter.Converter<string, double>(value, Double.TryParse);
        }

        public static decimal AsDecimal(string value)
        {
            return CommonConverter.Converter<string, decimal>(value, Decimal.TryParse);
        }

        public static decimal AsDecimal(string value, NumberStyles style, IFormatProvider provider)
        {
            return CommonConverter.Converter(value, (string e, out decimal k) => Decimal.TryParse(e, style, provider, out k));
        }

        public static T AsEnum<T>(string value) where T : struct, IEnumConstraint
        {
            return CommonConverter.Converter<string, T>(value, Enum.TryParse);
        }


        public static T AsEnum<T>(string value, bool ignoreCase) where T : struct, IEnumConstraint
        {
            return CommonConverter.Converter(value, (string e, out T result) => Enum.TryParse<T>(e, ignoreCase, out result));
        }


        public static DateTime AsDateTime(string value)
        {
            return CommonConverter.Converter<string, DateTime>(value, DateTime.TryParse);
        }

        public static DateTime AsDateTime(string value, DateTimeStyles style, IFormatProvider provider)
        {
            return CommonConverter.Converter(value, (string e, out DateTime k) => DateTime.TryParse(e, provider, style, out k));
        }

        public static DateTime AsDateTimeExact(string value, string format, DateTimeStyles style, IFormatProvider provider)
        {
            return CommonConverter.Converter(value, (string e, out DateTime k) => DateTime.TryParseExact(e, format, provider, style, out k));
        }

        public static DateTime AsDateTimeExact(string value, string[] formats, DateTimeStyles style, IFormatProvider provider)
        {
            return CommonConverter.Converter(value, (string e, out DateTime k) => DateTime.TryParseExact(e, formats, provider, style, out k));
        }
    }
}
