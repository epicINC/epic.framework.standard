using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Validators
{
    public class Validator
    {

        public static bool Any<T>(IEnumerable<T> collection, Predicate<T> predicate, Action<T, int> catcher = null)
        {
            for(var i = 0; i < collection.Count(); i++)
            {
                if (predicate(collection.ElementAt(i)))
                {
                    if (catcher != null) catcher(collection.ElementAt(i), i);
                    return true;
                }
            }
            return false;
        }

        public static bool IsNullOrWhiteSpace(params string[] value)
        {
            return Any(value, String.IsNullOrWhiteSpace);
        }

        public static bool IsNullOrEmpty(params string[] value)
        {
            return Any(value, String.IsNullOrWhiteSpace);
        }

        public static bool IsNull(params object[] value)
        {
            return Any(value, e => e == null);
        }
    }
}
