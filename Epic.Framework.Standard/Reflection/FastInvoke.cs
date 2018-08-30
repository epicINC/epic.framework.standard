using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Linq;

namespace Epic.Reflection
{
    /// <summary>
    /// https://www.codeproject.com/Articles/14593/A-General-Fast-Method-Invoker
    /// </summary>
    public static class FastInvoke
    {
        public static Action<object, object[]> Create(MethodInfo value)
        {
            var dynamicMethod = new DynamicMethod(String.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, value.DeclaringType.Module);
            var il = dynamicMethod.GetILGenerator();
            var parameters = value.GetParameters();
            var parameterTypes = parameters.Select(e => e.ParameterType.IsByRef ? e.ParameterType.GetElementType() : e.ParameterType).ToArray();
            var locals = parameterTypes.Select(e => il.DeclareLocal(e, true)).ToArray();

            for (int i = 0; i < parameterTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                Ldc(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                Unbox(il, parameterTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            if (!value.IsStatic)
                il.Emit(OpCodes.Ldarg_0);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    il.Emit(OpCodes.Ldloc, locals[i]);
            }

            if (value.IsStatic)
                il.EmitCall(OpCodes.Call, value, null);
            else
                il.EmitCall(OpCodes.Callvirt, value, null);

            if (value.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                Box(il, value.ReturnType);

            for (int i = 0; i < parameters.Length; i++)
            {
                if (!parameters[i].ParameterType.IsByRef) continue;
                il.Emit(OpCodes.Ldarg_1);
                Ldc(il, i);
                il.Emit(OpCodes.Ldloc, locals[i]);
                if (!locals[i].LocalType.IsValueType)
                    il.Emit(OpCodes.Box, locals[i].LocalType);
                il.Emit(OpCodes.Stelem_Ref);

            }
            il.Emit(OpCodes.Ret);
            return (Action<object, object[]>)dynamicMethod.CreateDelegate(typeof(Action<object, object[]>));

        }



        static void Ldc(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            else
                il.Emit(OpCodes.Ldc_I4, value);
        }

        static void Unbox(ILGenerator il, Type type)
        {
            if (type.IsValueType)
                il.Emit(OpCodes.Unbox_Any, type);
            else
                il.Emit(OpCodes.Castclass, type);
        }

        static void Box(ILGenerator il, Type type)
        {
            if (!type.IsValueType) return;
            il.Emit(OpCodes.Box, type);
        }
    }
}
