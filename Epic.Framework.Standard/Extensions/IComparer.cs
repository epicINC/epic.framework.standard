using Epic.Collections;
using System;


namespace System.Collections.Generic
{
    public static class IComparerExtensions
    {

        public static void Sort<T>(this List<T> value, Func<T, T, int> comparer)
        {
            value.Sort(new DynamicComparer<T>(comparer));
        }

        public static void Sort<T>(this List<T> value, int index, int count, Func<T, T, int> comparer)
        {
            value.Sort(index, count, new DynamicComparer<T>(comparer));
        }

    }
}
