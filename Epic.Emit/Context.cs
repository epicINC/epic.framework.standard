using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    internal class Context
    {
        public Context(ILGenerator value)
        {
            this.IL = value;
        }

        ILGenerator IL;


        public Context Newobj(ConstructorInfo value)
        {
            this.IL.Emit(OpCodes.Newobj, value);
            return this;
        }


        public Context CallorVirt(MethodInfo value)
        {
            if (value.IsVirtual)
                this.Callvirt(value);

            return this.Call(value);
        }

        public Context Callvirt(MethodInfo value)
        {
            this.IL.Emit(OpCodes.Callvirt, value);
            return this;
        }

        public Context Call(MethodInfo value)
        {
            this.IL.Emit(OpCodes.Call, value);
            return this;
        }



        bool FastLdarg(short index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException("index");

            if (index > Cache.Ldarg.Length) return false;
            this.IL.Emit(Cache.Ldarg[index]);
            return true;
        }


        public Context Ldarg(byte index)
        {
            if (!this.FastLdarg(index))
                this.IL.Emit(OpCodes.Ldarg_S, index);
            return this;
        }

        public Context Ldarg(short index)
        {
            if (!this.FastLdarg(index))
                this.IL.Emit(OpCodes.Ldarg, index);
            return this;
        }


        bool FastLdc(int value)
        {
            if (value < -1 || value > Cache.Ldc.Length) return false;

            if (value == -1)
                this.IL.Emit(OpCodes.Ldc_I4_M1);
            else
                this.IL.Emit(Cache.Ldc[value]);
            return true;
        }

        public Context Ldc(byte value)
        {
            if (!this.FastLdc(value))
                this.IL.Emit(OpCodes.Ldc_I4_S, value);
            return this;
        }

        public Context Ldc(int value)
        {
            if (!this.FastLdc(value))
                this.IL.Emit(OpCodes.Ldc_I4, value);
            return this;
        }

        public Context Ldc(long value)
        {
            this.IL.Emit(OpCodes.Ldc_I8, value);
            return this;
        }

        public Context Ldc(float value)
        {
            this.IL.Emit(OpCodes.Ldc_R4, value);
            return this;
        }

        public Context Ldc(double value)
        {
            this.IL.Emit(OpCodes.Ldc_R8, value);
            return this;
        }

        public Context Ld(string value)
        {
            this.IL.Emit(OpCodes.Ldstr, value);
            return this;
        }


        public Context Nop()
        {
            this.IL.Emit(OpCodes.Nop);
            return this;
        }

        public Context Dup()
        {
            this.IL.Emit(OpCodes.Dup);
            return this;
        }

        public Context Ret()
        {
            this.IL.Emit(OpCodes.Ret);
            return this;
        }
    }
}
