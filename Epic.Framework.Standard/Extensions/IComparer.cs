using System;

namespace System.Collections.Generic
{
    public static class IComparerExtensions
    {

        public static void Sort<T>(this List<T> value, Func<T, T, int> comparer)
        {
            value.Sort(new ComparerFunc<T>(comparer));
        }

        public static void Sort<T>(this List<T> value, int index, int count, Func<T, T, int> comparer)
        {
            value.Sort(index, count, new ComparerFunc<T>(comparer));
        }

    }


    internal class ComparerFunc<T> : IComparer<T>
    {
        public ComparerFunc(Func<T, T, int> comparer)
        {
            this.Comparer = comparer;
        }

        Func<T, T, int> Comparer;

        public int Compare(T x, T y)
        {
            return this.Comparer(x, y);
        }
    }


}
