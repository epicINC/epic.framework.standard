using Epic.Components;
using System;
using System.Collections.Generic;
using System.Text;



namespace Epic.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsFlags<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.IsFlags;
        }

        public static Type Type<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Type;
        }

        public static Type UnderlyingType<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.UnderlyingType;
        }


        public static T And<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }


        public static T Or<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }


        public static T Xor<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }



        public static T Not<T>(this T value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this byte value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this int value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this long value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }



        public static T Set<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }



        public static T Add<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }


        public static T Remove<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }



        public static bool HasValue<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }
    }
}
