using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Epic
{
    public static class JReflect
    {
        static Dictionary<MethodInfo, Invoker> Cache = new Dictionary<MethodInfo, Invoker>();

        public static object Apply(Delegate target, object @this)
        {
            return Apply(target.Method, @this, null);
        }

        public static object Apply(Delegate target, object[] args)
        {
            return Apply(target.Method, target.Target, args);
        }

        public static object Apply(Delegate target, object @this, object[] args)
        {
            return Apply(target.Method, @this, args);
        }



        public static object Apply(MethodInfo target, object @this, object[] args)
        {
            if (!Cache.TryGetValue(target, out Invoker result))
                Cache.Add(target, result = new Invoker(target));
            return result.Apply(@this, args);
        }
    }


    internal class Invoker
    {
        public Invoker(MethodInfo method)
        {
            this.Method = method;
            this.Parameters = method.GetParameters();
        }

        static readonly object[] EmptyArgs = new object[0];

        MethodInfo Method { get; set; }
        ParameterInfo[] Parameters { get; set; }

        Func<object, object[], object> FastMethod { get; set; }

        int Count { get; set; }

        public object Apply(object @this, object[] args)
        {

            if (this.Count < 2) return this.Method.Invoke(@this, this.SafeArgs(args));
            if (this.FastMethod == null)
                this.FastMethod = Epic.Reflection.FastInvoke.MethodProxy(this.Method);
            return this.FastMethod(@this, this.SafeArgs(args));
        }

        public object[] SafeArgs(object[] args)
        {
            var result = new object[this.Parameters.Length];
            if (args == null || args.Length == 0) return result;

            for(var i = 0; i < args.Length; i++)
                result[i] = ConvertType(this.Parameters[i].ParameterType, args[i]?.GetType(), args[i]);

            return result;
        }

        object ConvertType(Type target, Type source, object value)
        {
            if (value == null) return null;
            if (target == source) return value;
            try
            {
                return Convert.ChangeType(value, target);
            }
            catch (InvalidCastException)
            {

                return null;
            }
            
        }
        
    }
}
