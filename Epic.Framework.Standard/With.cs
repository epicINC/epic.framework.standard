using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic
{
    public class Magic
    {
        public static T With<T>(T value, Action<T> action)
        {
            action(value);
            return value;
        }

        public static K With<T, K>(T value, Func<T, K> conveter)
        {
            return conveter(value);
        }

        public static Func<Func<T, K>, K> With<T, K>(T value)
        {
            return (Func < T, K > e) => With(value, e);
        }

        public static T ContinueWith<T>(T value, Func<T, bool> predicate)
        {
            return predicate(value) ? value : default;
        }

        public static Func<Func<T, bool>, T> ContinueWith<T>(T value)
        {
            return (Func<T, bool> e) => ContinueWith(value, e);
        }

        static void WithTest()
        {
            With("1", e => e.Length);
            With<string, int>("1")(e => e.Length);
        }
    }
}
