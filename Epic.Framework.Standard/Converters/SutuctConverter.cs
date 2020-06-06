using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.Converters
{



    public class SutuctConverter
    {
        public static byte[] FastSerialize<T>(T value) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var result = new byte[size];
            GCHandle handle = new GCHandle();
            try
            {
                handle = GCHandle.Alloc(result, GCHandleType.Pinned);
                Marshal.StructureToPtr<T>(value, handle.AddrOfPinnedObject(), false);
            }
            finally
            {
                if (handle != null && handle.IsAllocated) handle.Free();
            }
            return result;
        }

        public static T FastDeserialize<T>(byte[] value) where T : struct
        {
            GCHandle handle = new GCHandle();
            try
            {
                handle = GCHandle.Alloc(value, GCHandleType.Pinned);
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                if (handle != null && handle.IsAllocated) handle.Free();
            }
        }
        
        public static byte[] Serialize<T>(T value) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr<T>(value, buffer, false);
                var result = new byte[size];
                Marshal.Copy(buffer, result, 0, size);
                return result;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static T Deserialize<T>(byte[] value)
        {
            var size = Marshal.SizeOf<T>();
            var buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(value, 0, buffer, size);
                return Marshal.PtrToStructure<T>(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }


 
    }
}
