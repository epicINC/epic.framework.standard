using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Converters
{
    public static class DictionaryConverter
    {
        public static string AsQueryString<T, K>(this IDictionary<T, K> dictionary, bool removeEmptyEntry = false)
        {
            if (removeEmptyEntry)
                return AsQueryString(dictionary, RemoveEmptyEntry);
            return String.Join("&", dictionary.Select(e => e.Key + "=" + e.Value));
        }


        static string AsQueryString<T, K>(this IDictionary<T, K> dictionary, Func<KeyValuePair<T, K>, bool> filter)
        {
            if (filter != null)
                return String.Join("&", dictionary.Where(filter).Select(e => e.Key + "=" + e.Value));


            return String.Join("&", dictionary.Select(e => e.Key + "=" + e.Value));
        }

        internal static bool RemoveEmptyEntry<T, K>(KeyValuePair<T, K> e)
        {
            return e.Value != null && !e.Value.Equals(String.Empty);
        }

    }
}
