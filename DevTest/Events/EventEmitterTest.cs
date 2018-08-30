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
            var e = new EventEmitter();
            Action a1;
            e.On("event", a1 = () =>
            {
                Console.WriteLine("no arg");
            });
            e.On("event", (bool value) =>
            {
                Console.WriteLine("arg1");
            });
            e.On("event", (bool value, string a) =>
            {
                Console.WriteLine(value);
                Console.WriteLine(a);
            });
            e.Off("event", a1);
            e.Emit("event", true);
        }
    }
}
