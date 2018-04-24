using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.Tips
{


    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct FloatBytes
    {
        [FieldOffset(0)]
        public byte Index0;
        [FieldOffset(1)]
        public byte Index1;
        [FieldOffset(2)]
        public byte Index2;
        [FieldOffset(3)]
        public byte Index3;

        [FieldOffset(0)]
        public int i;

        [FieldOffset(0)]
        public float Value;
    }

    class StructDemo
    {
        static void Demo()
        {
            var result = new FloatBytes();
            result.Value = 0.4f;

            Console.WriteLine(result.Index0);
        }
    }
}
