using Epic.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTest.Reflection
{
    class FastInvokeTest
    {
        public static void Test()
        {
            
            var result = Epic.JReflect.Apply((Func<string, int, bool>)TSR, null);
            Console.WriteLine(result);

            Epic.JReflect.Apply((Action<string, int>)TS, null);

            var current = new FastInvokeTest("a");
            var next = new FastInvokeTest("b");

            result = Epic.JReflect.Apply((Func<string, int, bool>)current.TDR, null);
            Console.WriteLine(result);

            Epic.JReflect.Apply((Action<string, int>)current.TD, null);

            result = Epic.JReflect.Apply((Func<string, int, bool>)current.TDR, next);
            Console.WriteLine(result);

            Epic.JReflect.Apply((Action<string, int>)current.TD, next);

        }

        public static void MethodProxy()
        {

            Action<bool> action = e =>
            {
                Console.WriteLine(e +", "+ DateTime.Now.ToString());
            };

            var result = FastInvoke.MethodProxy(action);

            result(action.Method, new object[] { false });
        }


        static Action<bool> action;

        static void CallVal()
        {
            action(false);
        }



        FastInvokeTest(string v)
        {
            this.v = v;
        }

        public static object TSRCall(object target, object[] args)
        {
            return TSR((string)args[0], (int)args[1]);

        }

        public static void TSCall(object target, object[] args)
        {
            TS((string)args[0], (int)args[1]);

        }

        public static void TS(string a, int b)
        {
            Console.WriteLine($"静态方法无返回值:{a}, {b}");
        }

        public static bool TSR(string a, int b)
        {
            Console.WriteLine($"静态返回有返回值:{a}, {b}");
            return true;
        }


        public object TDRCall(object target, object[] args)
        {
            return TDR((string)args[0], (int)args[1]);
        }

        public void TDCall(object target, object[] args)
        {
            TD((string)args[0], (int)args[1]);
        }

        string v;

        public void TD(string a, int b)
        {
            Console.WriteLine($"动态方法无返回值:{v} {a}, {b}");
        }

        public bool TDR(string a, int b)
        {
            Console.WriteLine($"动态方法有返回值:{v} {a}, {b}");
            return true;
        }
    }

}
