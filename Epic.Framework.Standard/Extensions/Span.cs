using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class SpanExtensions
    {
        public static Span<T> SafeSlice<T>(this Span<T> value, int start, int length)
        {
            if (start > value.Length) return new Span<T>();

            var end = start + length;
            if (end > value.Length)
                return value.Slice(start, value.Length - start);
            return value.Slice(start, length);
        }
    }
}
