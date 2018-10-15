using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace System.Reflection.Emit
{
    public static class Extensions
    {
        public static T CreateDelegate<T>(this DynamicMethod value) where T : Delegate
        {
            return (T)value.CreateDelegate(typeof(T));
        }
    }
}
