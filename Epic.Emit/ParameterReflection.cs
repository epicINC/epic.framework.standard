using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Epic.Emit
{
    internal class ParameterReflection
    {
        public ParameterReflection(Context context, ParameterInfo parameter)
        {
            this.Context = context;
            this.Parameter = parameter;
        }


        Context Context;
        ParameterInfo Parameter;

        public string Name { get => this.Parameter.Name; }
        public int Position { get => this.Parameter.Position; }
        public Type Type { get => this.Parameter.ParameterType; }



        public ClassReflection Class()
        {
            return new ClassReflection()
        }

    }
}
