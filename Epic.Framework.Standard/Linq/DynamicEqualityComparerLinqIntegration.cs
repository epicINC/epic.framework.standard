using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic;
using Epic.Collections;

namespace System.Linq
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities
    /// </summary>
    public static class DynamicEqualityComparerLinqIntegration
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> value, Func<T, T, bool> equalityComparer) where T : class
        {
            return value.Distinct(new DynamicEqualityComparer<T>(equalityComparer));
        }

        public static IEnumerable<IGrouping<T, T>> GroupBy<T>(this IEnumerable<T> value, Func<T, T, bool> equalityComparer) where T : class
        {
            return value.GroupBy((T t) => t, new DynamicEqualityComparer<T>(equalityComparer));
        }

        static void CheckParameter(object source, object dest, object equalityComparer)
        {
            Errors.CheckArgumentNull("source", source);
            Errors.CheckArgumentNull("dest", dest);
            Errors.CheckArgumentNull("equalityComparer", equalityComparer);
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> source, IEnumerable<T> dest, Func<T, T, bool> equalityComparer, Func<T, int> getHashCode = null)
        {
            CheckParameter(source, dest, equalityComparer);

            if (source.Count() == 0) return dest;
            if (dest.Count() == 0) return source;
            return source.Union(dest, new DynamicEqualityComparer<T>(equalityComparer, getHashCode));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> dest, Func<T, T, bool> equalityComparer, Func<T, int> getHashCode = null) where T : class
        {
            CheckParameter(source, dest, equalityComparer);

            if (source.Count() == 0) return dest;
            if (dest.Count() == 0) return source;
            return source.Intersect(dest, new DynamicEqualityComparer<T>(equalityComparer, getHashCode));
        }

    }
}
