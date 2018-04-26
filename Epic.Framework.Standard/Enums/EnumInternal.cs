using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Caches;
using Epic.Components;

namespace Epic.Enums
{


    internal static class EnumInternal<T> where T : struct, IEnumConstraint
    {


        static EnumInternal()
        {
            Type = typeof(T);
            UnderlyingType = Type.GetEnumUnderlyingType();
            IsFlags = Type.IsDefined(typeof(FlagsAttribute), false);

            EpicRuntimeCache<IEnumOperation<T, T>>.Init = () => new EnumOperation<T, T>();
            EpicRuntimeCache<IEnumOperation<T, byte>>.Init = () => new EnumOperation<T, byte>();
            EpicRuntimeCache<IEnumOperation<T, int>>.Init = () => new EnumOperation<T, int>();
            EpicRuntimeCache<IEnumOperation<T, long>>.Init = () => new EnumOperation<T, long>();

        }

        internal static System.Enum Mark()
        {
            return null;
        }


        public static bool IsFlags
        {
            get;
            private set;
        }

        public static Type Type
        {
            get;
            private set;
        }

        public static Type UnderlyingType
        {
            get;
            private set;
        }


        public static IEnumOperation<T, T> Operation
        {
            get { return EpicRuntimeCache<IEnumOperation<T, T>>.Value; }
        }

        public static IEnumOperation<T, byte> OperationByte
        {
            get { return EpicRuntimeCache<IEnumOperation<T, byte>>.Value; }
        }

        public static IEnumOperation<T, int> OperationInt
        {
            get { return EpicRuntimeCache<IEnumOperation<T, int>>.Value; }
        }

        public static IEnumOperation<T, long> OperationLong
        {
            get { return EpicRuntimeCache<IEnumOperation<T, long>>.Value; }
        }




        public static T And(T left, T right)
        {
            return Operation.And(left, right);
        }

        public static T And(T left, byte right)
        {
            return OperationByte.And(left, right);
        }

        public static T And(T left, int right)
        {
            return OperationInt.And(left, right);
        }

        public static T And(T left, long right)
        {
            return OperationLong.And(left, right);
        }


        public static T Or(T left, T right)
        {
            return Operation.Or(left, right);
        }

        public static T Or(T left, byte right)
        {
            return OperationByte.Or(left, right);
        }

        public static T Or(T left, int right)
        {
            return OperationInt.Or(left, right);
        }

        public static T Or(T left, long right)
        {
            return OperationLong.Or(left, right);
        }


        public static T Xor(T left, T right)
        {
            return Operation.Xor(left, right);
        }

        public static T Xor(T left, byte right)
        {
            return OperationByte.Xor(left, right);
        }

        public static T Xor(T left, int right)
        {
            return OperationInt.Xor(left, right);
        }

        public static T Xor(T left, long right)
        {
            return OperationLong.Xor(left, right);
        }



        public static T Not(T value)
        {
            return Operation.Not(value);
        }

        public static T Not(byte value)
        {
            return OperationByte.Not(value);
        }

        public static T Not(int value)
        {
            return OperationInt.Not(value);
        }

        public static T Not(long value)
        {
            return OperationLong.Not(value);
        }



        public static T Set(T left, T right)
        {
            return Operation.Set(left, right);
        }

        public static T Set(T left, byte right)
        {
            return OperationByte.Set(left, right);
        }

        public static T Set(T left, int right)
        {
            return OperationInt.Set(left, right);
        }

        public static T Set(T left, long right)
        {
            return OperationLong.Set(left, right);
        }


        public static T Set(T left, T right, bool addOrRemove)
        {
            if (addOrRemove)
                return Operation.Add(left, right);
            else
                return Operation.Remove(left, right);
        }

        public static T Set(T left, byte right, bool addOrRemove)
        {
            if (addOrRemove)
                return OperationByte.Add(left, right);
            else
                return OperationByte.Remove(left, right);
        }

        public static T Set(T left, int right, bool addOrRemove)
        {
            if (addOrRemove)
                return OperationInt.Add(left, right);
            else
                return OperationInt.Remove(left, right);
        }

        public static T Set(T left, long right, bool addOrRemove)
        {
            if (addOrRemove)
                return OperationLong.Add(left, right);
            else
                return OperationLong.Remove(left, right);
        }



        public static T Add(T left, T right)
        {
            return Operation.Add(left, right);
        }

        public static T Add(T left, byte right)
        {
            return OperationByte.Add(left, right);
        }

        public static T Add(T left, int right)
        {
            return OperationInt.Add(left, right);
        }

        public static T Add(T left, long right)
        {
            return OperationLong.Add(left, right);
        }


        public static T Remove(T left, T right)
        {
            return Operation.Remove(left, right);
        }

        public static T Remove(T left, byte right)
        {
            return OperationByte.Remove(left, right);
        }

        public static T Remove(T left, int right)
        {
            return OperationInt.Remove(left, right);
        }

        public static T Remove(T left, long right)
        {
            return OperationLong.Remove(left, right);
        }



        public static bool HasValue(T left, T right)
        {
            return Operation.HasValue(left, right);
        }

        public static bool HasValue(T left, byte right)
        {
            return OperationByte.HasValue(left, right);
        }

        public static bool HasValue(T left, int right)
        {
            return OperationInt.HasValue(left, right);
        }

        public static bool HasValue(T left, long right)
        {
            return OperationLong.HasValue(left, right);
        }

    }


}
