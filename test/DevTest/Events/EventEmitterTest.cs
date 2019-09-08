using Epic.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTest.Events
{
    class EventEmitterTest
    {
        public static void Combine()
        {
            test();
        }


        static void test()
        {
            var e = new EventEmitter();

            e.On("event", (bool args1, string args2) =>
            {
                Console.WriteLine(args1);
            });

            e.Emit("event", true, "test1");
            e.Emit("event", false, "test2");

        }


    }
}
