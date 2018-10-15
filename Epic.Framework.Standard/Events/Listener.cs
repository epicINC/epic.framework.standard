using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Epic.Events
{
    public class Listener
    {

        public Listener(Delegate action)
        {
            this.Action = action;
        }

        public void Invoke(params object[] args)
        {
            if (this.Count > -1)
                this.DynamicInvoke(args);
            else
                this.FastInvoke(args);

            this.Count++;
            this.OnInvoked();
        }

        void DynamicInvoke(params object[] args)
        {
            this.Action.DynamicInvoke(this.MakeArgs(args));
        }

        void FastInvoke(params object[] args)
        {
            if (this.FastAction == null)
                this.FastAction = Epic.Reflection.FastInvoke.MethodProxy(this.Method);

            if (this.Action != null)
                this.FastAction(this.Action, this.MakeArgs(args));
            else
                this.FastAction(this.Method, this.MakeArgs(args));
        }

        object[] MakeArgs(object[] args)
        {
            if (this.Parameters.Length == 0) return null;
            if (this.IsSame(args)) return args;
            var result = new object[this.Parameters.Length];
            var count = args.Length < result.Length ? args.Length : result.Length;
            for (var i = 0; i < count; i++)
            {
                if (this.parameters[i].ParameterType != args[i].GetType()) continue;
                result[i] = args[i];
            }
            return result;
        }
 
        public event Action Invoked;
        void OnInvoked()
        {
            if (this.Invoked == null) return;
            this.Invoked();
        }

        public bool IsSame(params object[] args)
        {
            if (this.ParameterCount != args.Length) return false;
            return this.Parameters.SequenceEqual(args, (p, a) => p.ParameterType == a.GetType());
        }


        public Delegate Action { get; private set; }
        Func<object, object[], object> FastAction { get; set; }


        MethodInfo method;
        MethodInfo Method
        {
            get
            {
                if (this.method != null) return this.method;
                return this.method = this.Action.Method;
            }

        }

        ParameterInfo[] parameters;
        public ParameterInfo[] Parameters
        {
            get
            {
                if (this.parameters != null) return this.parameters;
                return this.parameters = this.Action.Method.GetParameters();
            }
        }

        public int ParameterCount { get => this.Parameters.Length; }

        public int Count { get; private set; }

    }

}
