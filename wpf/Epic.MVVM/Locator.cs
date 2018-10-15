using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.MVVM
{
    public static class Locator
    {
        public static T Instance<T>() where T : new()
        {
            if (Box<T>.Value != null) return Box<T>.Value;
            return Box<T>.Value = new T();
        }

        public static T Instance<T>(Func<T> createInstance)
        {
            if (Box<T>.Value != null) return Box<T>.Value;
            return Box<T>.Value = createInstance();
        }

        static class Box<T>
        {
            internal static T Value;
        }
    }

}
