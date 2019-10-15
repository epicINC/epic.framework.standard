using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ArrayExtensions
    {

        public static ArraySegment<T>[] Split<T>(this T[] data, int step, Action<ArraySegment<T>> fn = null)
        {
            return Split<T>(data, 0, step, fn);
        }

        public static ArraySegment<T>[] Split<T>(this T[] data, int step, Action<ArraySegment<T>, int> fn = null)
        {
            return Split<T>(data, 0, step, fn);
        }

        public static ArraySegment<T>[] Split<T>(this T[] data, int step, Action<ArraySegment<T>, int, int> fn = null)
        {
            return Split<T>(data, 0, step, fn);
        }

        public static ArraySegment<T>[] Split<T>(this T[] data, int offset, int step, Action<ArraySegment<T>> fn = null)
        {
            if (fn != null) return Split<T>(data, offset, step, (item, i, count) => fn(item));
            return Split<T>(data, offset, step, default(Action<ArraySegment<T>, int, int>));
        }

        public static ArraySegment<T>[] Split<T>(this T[] data, int offset, int step, Action<ArraySegment<T>, int> fn)
        {
            if (fn != null) return Split<T>(data, offset, step, (item, i, count) => fn(item, i));
            return Split<T>(data, offset, step, default(Action<ArraySegment<T>, int, int>));
        }

        public static ArraySegment<T>[] Split<T>(this T[] data, int offset, int step, Action<ArraySegment<T>, int, int> fn)
        {
            if (data == null || data.Length == 0) return default;
            if (offset < 0 || step < 0) return default;

            int count = unchecked((int)Math.Ceiling((Double)(data.Length - offset) / step)), upper = count - 1;
            var result = new ArraySegment<T>[count];

            if (fn != null)
            {
                for (byte i = 0; ; i++)
                {
                    if (upper == i)
                    {
                        fn(result[i] = new ArraySegment<T>(data, offset = i * step + offset, data.Length - offset), i, count);
                        break;
                    }
                    fn(result[i] = new ArraySegment<T>(data, i * step + offset, step), i, count);
                }
            }
            else
            {
                for (byte i = 0; ; i++)
                {
                    if (upper == i)
                    {
                        result[i] = new ArraySegment<T>(data, offset = i * step + offset, data.Length - offset);
                        break;
                    }
                    result[i] = new ArraySegment<T>(data, i * step + offset, step);
                }
            }

            return result;
        }
    }
}
