using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.FluentAPI
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities.PropertyInfoExtensions 略做修改
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        public static bool IsSame(this PropertyInfo value, PropertyInfo other)
        {
            return value.DeclaringType == other.DeclaringType && value.Name == other.Name;
        }

        public static T GetCustomAttributes<T>(this PropertyInfo value, bool inherit = true, int index = 0) where T : class
        {
            var result = value.GetCustomAttributes(typeof(T), inherit);
            if (result == null || result.Length == 0) return null;
            return result[index] as T;
        }

    }
}
