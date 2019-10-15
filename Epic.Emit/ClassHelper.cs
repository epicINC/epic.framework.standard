using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Epic.Emit
{
    public class ClassHelper
    {


        public static ConstructorInfo FindCtor(Type instance)
        {
            return FindCtor(instance, Type.EmptyTypes);
        }

        public static ConstructorInfo FindCtor(Type instance, params Type[] types)
        {
            return instance.GetConstructor(types);
        }

        public static PropertyInfo FindProperty(Type instance, string name)
        {
            return instance.GetProperty(name);
        }



    }
}
