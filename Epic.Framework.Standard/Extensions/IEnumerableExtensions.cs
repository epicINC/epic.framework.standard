using Epic.Collections;
using Epic.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class IEnumerableExtensions
    {

        public static IEnumerable<T> Random<T>(this IEnumerable<T> value)
        {
            var random = new Random();
            return value.OrderBy(e => random.Next());
        }

        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            ForEach<T>(value, (e, i) => action(e));
        }

        public static void ForEach<T>(this IEnumerable<T> value, Action<T, int> action)
        {
            Errors.ArgumentNull("action", action);
            var i = 0;
            foreach (var item in value)
                action(item, i++);
        }


        public static IEnumerable<T> TryCast<T>(this IEnumerable source) where T : class
        {
            T result;
            foreach (var item in source)
            {
                result = item as T;
                if (result == null)
                    continue;
                yield return result;
            }
        }


        public static IEnumerable<K> TryCast<T, K>(this IEnumerable<T> source, TryParser<T, K> parser)
        {
            K result;
            foreach (var item in source)
            {
                if (parser(item, out result))
                    yield return result;
            }
        }



        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer)
        {
            return source.Intersect(target, new DynamicEqualityComparer<T>(comparer));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer, out IEnumerable<T> except)
        {
            except = Except<T>(source, target, comparer);
            return Intersect<T>(source, target, comparer);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer)
        {
            return source.Except(target, new DynamicEqualityComparer<T>(comparer));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer, out IEnumerable<T> intersect)
        {
            intersect = Intersect<T>(source, target, comparer);
            return Except<T>(source, target, comparer);
        }


        public static IEnumerable<T> Intersect<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            foreach (var item in source)
            {
                foreach (var local in target)
                {
                    if (!comparer(item, local)) continue;
                    yield return item;
                    break;
                }
            }
        }

        public static IEnumerable<T> Intersect<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer, out IEnumerable<T> except)
        {
            var result = Intersect<T, K>(source, target, comparer);
            except = source.Except(result);
            return result;
        }

        public static IEnumerable<T> Except<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            return source.Except(Intersect<T, K>(source, target, comparer));
        }

        public static IEnumerable<T> Except<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer, out IEnumerable<T> intersect)
        {
            intersect = Intersect<T, K>(source, target, comparer);
            return source.Except(intersect);
        }


    }
}
