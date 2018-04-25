using Epic.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Extensions
{
    public static class EnumExtensions
    {
        public enum Test
        {
            one,
            two,
        }



        public static bool Has<T>(this T value, T expected) where T : struct, IEnumConstraint
        {
            return (Test.one & Test.two) == Test.two;
        }
    }
}
