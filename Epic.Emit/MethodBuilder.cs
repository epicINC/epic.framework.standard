using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Epic.Emit
{
    internal class MethodBuilder
    {

        public MethodBuilder(DynamicMethod method)
        {
            this.Method = method;

        }

        DynamicMethod Method { get; set; }

        Context Context { get; set; }


        public ParameterReflection Parameter(int index)
        {
            var p = this.Method.GetParameters()[index];
            return new ParameterReflection(p.Name, p.Position, p.ParameterType);
        }



        public static MethodBuilder Build(Type returnType, params Type[] parameterTypes)
        {
            return new MethodBuilder(new DynamicMethod("test", returnType, parameterTypes));
        }
    }
}
