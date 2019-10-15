using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Epic.Emit
{

    public class ClassReflection
    {

        internal ClassReflection(Context value, Type type)
        {
            this.Context = value;
            this.Instance = type;
        }

        Context Context;
        Type Instance;


        public Action Ld { get; set; }

        public ClassReflection Ctor(ConstructorInfo value)
        {
            this.Context.Newobj(value);
            return this;
        }

        public ClassReflection Ctor()
        {
            return this.Ctor(ClassHelper.FindCtor(this.Instance));
        }


        public ClassReflection Set(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (!property.CanWrite)
                throw new InvalidOperationException("Property is not writable " + property.Name);

            var method = property.GetSetMethod(true);
            if (!method.IsStatic) this.Ld();

            this.Context.CallorVirt(property.GetSetMethod(true));
            return this;
        }

        public ClassReflection Set(string name, int value)
        {
            return this.Set(ClassHelper.FindProperty(this.Instance, name));
        }

    }
}
