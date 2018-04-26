using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Collections
{


    /// <summary>
    /// IEqualityComparer 动态实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicEqualityComparer<T> : IEqualityComparer<T>
    {
        public DynamicEqualityComparer(Func<T, T, bool> func, Func<T, int> getHashCode = null)
        {
            this.func = func;
            this.getHashCode = getHashCode;
        }

        Func<T, T, bool> func;
        Func<T, int> getHashCode;

        public bool Equals(T x, T y)
        {
            return func(x, y);
        }

        public int GetHashCode(T value)
        {
            if (this.getHashCode == null)
                return value.GetHashCode();
            return this.getHashCode(value);
        }
    }
}
