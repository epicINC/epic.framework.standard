using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Extensions
{
    public static class EnumerableExtensions
    {

        static void Test()
        {
            var a = new[] {1,2,3 };

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
        public static IEnumerable<TResult> CrossJoin<T, K, TResult>(this IEnumerable<T> left, IEnumerable<K> right, Func<T, K,TResult> selector)
        {
            return left.SelectMany(e => right, selector);
        }


    }
}
