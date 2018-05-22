using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Epic.Extensions
{
    public static class StringExtensions
    {
        public static bool IgnoreCaseEquals(this string value, string expected, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (value == null) return expected == null;
            return String.CompareOrdinal(value, expected) == 0;
        }



        public static string Repeat(this string value, int count)
        {
            if (count < 1) return String.Empty;
            var result = String.Empty;
            for (var i = 0; i < count; i++)
                result += value;
            return result;
        }

        public static string PadLeft(this string value, int targetLength, string padString = " ")
        {
            if (value.Length >= targetLength) return value;
            targetLength = targetLength - value.Length;
            if (targetLength > padString.Length)
                padString += padString.Repeat(targetLength / padString.Length);

            return padString.Substring(0, targetLength) + value;
        }

        public static string PadRight(this string value, int targetLength, string padString = " ")
        {
            if (value.Length >= targetLength) return value;
            targetLength = targetLength - value.Length;
            if (targetLength > padString.Length)
                padString += padString.Repeat(targetLength / padString.Length);
            return value + padString.Substring(0, targetLength);
        }

        public static string Slice(this string value, int beginIndex, int endIndex)
        {
            if (beginIndex < 0) beginIndex = value.Length + beginIndex;

            return value.Substring(beginIndex, endIndex - beginIndex);
        }


        public static bool IgnoreCaseEquals(this string value, string expected)
        {
            return String.Equals(value, expected, StringComparison.OrdinalIgnoreCase);
        }


        #region Regex

        public static bool IsMatch(this string value, string regex)
        {
            return IsMatch(value, new Regex(regex));
        }

        public static bool IsMatch(this string value, Regex regex)
        {
            return regex.IsMatch(value);
        }

        public static string Replace(this string value, Regex regex, string replacement)
        {
            return regex.Replace(value, replacement);
        }

        public static string Replace(this string value, Regex regex, Func<Match, string> replacement)
        {
            return regex.Replace(value, new MatchEvaluator(replacement));
        }

        public static MatchCollection Match(this string value, Regex regex)
        {
            return regex.Matches(value);
        }

        public static MatchCollection Match(this string value, string regex)
        {
            return Match(value, new Regex(regex));
        }


        #endregion
    }
}
