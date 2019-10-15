using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Epic.Emit
{

    internal static class Cache
    {
        static Cache()
        {

        }

        internal static readonly OpCode[] Ldc = new[]{
            OpCodes.Ldc_I4_0,
            OpCodes.Ldc_I4_1,
            OpCodes.Ldc_I4_2,
            OpCodes.Ldc_I4_3,
            OpCodes.Ldc_I4_4,
            OpCodes.Ldc_I4_5,
            OpCodes.Ldc_I4_6,
            OpCodes.Ldc_I4_7,
            OpCodes.Ldc_I4_8
        };

        internal static readonly OpCode[] Ldarg = new[]
        {
            OpCodes.Ldarg_0,
            OpCodes.Ldarg_1,
            OpCodes.Ldarg_2,
            OpCodes.Ldarg_3,
        };

    }
}
