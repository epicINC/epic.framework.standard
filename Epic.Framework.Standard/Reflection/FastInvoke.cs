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
        public static Func<object, object[], object> MethodProxy(Delegate value)
        {
            return MethodProxy(value.Method);
        }

        public static Func<object, object[], object> MethodProxy(MethodInfo value)
        {
            var dynamicMethod = new DynamicMethod(String.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, value.DeclaringType.Module);
            BuildProxy(value, dynamicMethod.GetILGenerator());
            return dynamicMethod.CreateDelegate<Func<object, object[], object>>();
        }

        /*
        public static Func<object, object[], object> MethodProxyS(MethodInfo value)
        {
            var assemblyName = new AssemblyName("ProxyAssembly");
            assemblyName.Version = new Version("1.0.0");
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            var moduleBuilder = assemblyBuilder.DefineDynamicModule("ProxyModule", "DynamicProxy.dll");

            TypeBuilder builder = moduleBuilder.DefineType("Test", TypeAttributes.Public);
            var methodBuilder = builder.DefineMethod("DynamicCreate", MethodAttributes.Public | MethodAttributes.Static, typeof(object), new Type[] { typeof(object), typeof(object[]) });

            BuildProxy(value, methodBuilder.GetILGenerator());

            builder.CreateType();
            assemblyBuilder.Save("DynamicProxy.dll");

            return (Func<object, object[], object>)methodBuilder.CreateDelegate(typeof(Func<object, object[], object>));
        }
        */


        static void BuildProxy(MethodInfo value, ILGenerator il)
        {
            var parameters = value.GetParameters();
            var parameterTypes = parameters.Select(e => e.ParameterType.IsByRef ? e.ParameterType.GetElementType() : e.ParameterType).ToArray();


            il.DeclareLocal(typeof(object));

            il.Emit(OpCodes.Nop);
            if (!value.IsStatic)
                il.Emit(OpCodes.Ldarg_0);

            for (int i = 0; i < parameterTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                Ldc(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                Unbox(il, parameterTypes[i]);
            }

            if (value.IsStatic)
                il.EmitCall(OpCodes.Call, value, null);
            else
                il.EmitCall(OpCodes.Callvirt, value, null);

            if (value.ReturnType != typeof(void))
            {
                Box(il, value.ReturnType);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Ldloc_0);
            }
            else
                il.Emit(OpCodes.Ldnull);

            il.Emit(OpCodes.Ret);

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
                il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
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
