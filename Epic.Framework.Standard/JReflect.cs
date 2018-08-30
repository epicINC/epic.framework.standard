using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Epic
{
    public static class JReflect
    {
        static Dictionary<MethodInfo, Invoker>
        public static dynamic Apply(Delegate target, object[] args)
        {
            return Apply(target.Method, target.Target, args);
        }

        static dynamic Apply(MethodInfo target, object @this, object[] args)
        {

            if (target.GetParameters().Length == 0)
                return target.Invoke(@this, null);



        }

    }


    internal class Invoker
    {
        public Invoker(MethodInfo method)
        {
            this.Method = method;
            this.Parameters = method.GetParameters();
        }

        MethodInfo Method { get; set; }
        ParameterInfo[] Parameters { get; set; }

        int Count { get; set; }

        public bool Apply(object @this, object[] args)
        {
            return false;
        }
        
    }
}
