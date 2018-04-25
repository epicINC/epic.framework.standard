using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.Win32
{
    internal class SutuctConverter
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe IntPtr AddressOf<T>(T value)
        {
            var reference = __makeref(value);
            return *(IntPtr*)(&reference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe IntPtr AddressOfRef<T>(ref T value)
        {
            var reference = __makeref(value);
            TypedReference* pRef = &reference;
            return (IntPtr)pRef; //(&pRef)
        }


        public struct Test
        {

        }

        public static unsafe byte[] Serialize(Test[] value)
        {
            var buffer = new byte[Marshal.SizeOf(typeof(Test)) * value.Length];
            fixed (void* d = &buffer[0])
            {
                fixed (void* s = &value[0])
                {
                    API.CopyMemory(d, s, buffer.Length);
                }
            }

            return buffer;
        }


        public static unsafe byte[] Serialize2(Test value)
        {
            var result = new byte[Marshal.SizeOf(typeof(Test))];
            Marshal.Copy((IntPtr)(&value), result, 0, result.Length);
            return result;
        }

        public static unsafe byte[] Serialize3<T>(T value)
        {
            var result = new byte[Marshal.SizeOf<T>()];
            Marshal.Copy(AddressOf(value), result, 0, result.Length);
            return result;
        }


        public static T Deserialize<T>(byte[] value)
        {
            return Marshal.PtrToStructure<T>(AddressOfRef(ref value));

        }
    }
}
