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
            if (this.Count < 3)
            {
                this.DynamicInvoke(args);
                this.Count++;
                this.OnInvoked();
                return;
            }

            this.FastInvoke(args);
            this.Count++;
            this.OnInvoked();
        }

        void DynamicInvoke(params object[] args)
        {
            if (this.Parameters.Length == 0)
            {
                this.Action.DynamicInvoke();
                return;
            }
            if (this.IsSame(args))
            {
                this.Action.DynamicInvoke(args);
                return;
            }

            this.Action.DynamicInvoke(new object[this.Parameters.Length]);
        }

        void FastInvoke(params object[] args)
        {
            if (this.FastAction == null)
                this.FastAction = Epic.Reflection.FastInvoke.Create(this.Method);

            if (this.Parameters.Length == 0)
            {
                this.FastAction(null, null);
                return;
            }
            if (this.IsSame(args))
            {
                this.FastAction(null, args);
                return;
            }

            this.FastAction(null, new object[this.Parameters.Length]);
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
        Action<object, object[]> FastAction { get; set; }


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
