using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Extensions
{
    public static class IDictionaryExtensions
    {
        public static void AddRange<T, K>(this IDictionary<T, K> dic, IEnumerable<KeyValuePair<T, K>> value)
        {
            value.ForEach(dic.Add);
        }
    }
}
