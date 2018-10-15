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

            e.On("event", (bool value) =>
            {
                Console.WriteLine(value);
            });

            e.Emit("event", true);
            e.Emit("event", false);

            e.Emit("event", true);
            e.Emit("event", false, "adfad");
        }


    }
}
