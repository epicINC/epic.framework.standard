using Epic;
using Epic.Collections;
using Epic.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class EnumerableIntegration
    {

        //public static bool Remove<T>(this ICollection<T> value, Func<T, bool> predicate)
        //{
        //    if (value.IsReadOnly) return false;

        //    return value.Where(predicate).ToList().Select(e => value.Remove(e)).All(e => e);
        //}

        public static bool SequenceEqual<T, K>(this IEnumerable<T> first, IEnumerable<K> second, Func<T, K, bool> comparer)
        {
            if (first == null) throw Errors.ArgumentNull("first");
            if (second == null) throw Errors.ArgumentNull("second");

            using (var enumerator = first.GetEnumerator())
            {
                using (var enumerator2 = second.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (!enumerator2.MoveNext() || !comparer(enumerator.Current, enumerator2.Current)) return false;
                    }
                    if (enumerator2.MoveNext()) return false;
                }
            }
            return true;
        }


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
            Errors.CheckArgumentNull("action", action);
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


        public static bool Contains<T>(this IEnumerable<T> value, Func<T, bool> comparer)
        {
            foreach (var item in value)
            {
                if (comparer(item)) return true;
            }
            return false;
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



        public static IEnumerable<TResult> LeftJoin<T, K, TKey, TResult>(this IEnumerable<T> left, IEnumerable<K> right, Func<T, TKey> leftKey, Func<K, TKey> rightKey, Func<T, K, TResult> selector)
        {
            return left
                .GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                .SelectMany(temp => temp.r.DefaultIfEmpty(), (temp, r) => selector(temp.l, r));
        }

        public static IEnumerable<TResult> RightJoin<T, K, TKey, TResult>(this IEnumerable<T> left, IEnumerable<K> right, Func<T, TKey> leftKey, Func<K, TKey> rightKey, Func<T, K, TResult> selector)
        {
            return right
            .GroupJoin(left, rightKey, leftKey, (r, l) => new { r, l })
            .SelectMany(temp => temp.l.DefaultIfEmpty(), (temp, r) => selector(r, temp.r));
        }


        public static IEnumerable<TResult> UnionAll<T, K, TKey, TResult>(this IEnumerable<T> left, IEnumerable<K> right, Func<T, TKey> leftKey, Func<K, TKey> rightKey, Func<T, K, TResult> selector)
        {
            return LeftJoin(left, right, leftKey, rightKey, selector)
                .Concat(RightJoin(left, right, leftKey, rightKey, selector))
                .Distinct();
        }

        /// <summary>
        /// 笛卡尔积
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> CrossJoin<T, K, TResult>(this IEnumerable<T> left, IEnumerable<K> right, Func<T, K, TResult> selector)
        {
            return left.SelectMany(e => right, selector);
        }


    }
}
