using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Collections
{
    internal class DynamicComparer<T> : IComparer<T>
    {
        public DynamicComparer(Func<T, T, int> comparer)
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
