using Epic.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Test.Events
{
    [TestClass]
    public class EventEmitterTest
    {
        public EventEmitterTest()
        {

        }

        

        [TestMethod]
        public void On()
        {
            var e = new EventEmitter();
            e.On("event", () =>
            {
                Assert.IsTrue(true);
            });
            e.Emit("event");
        }

        [TestMethod]
        public void OnWithArgs()
        {
            var e = new EventEmitter();
            e.On("event", (bool value) =>
            {
                Assert.IsTrue(value);
            });
            e.Emit("event", true);
        }

        [TestMethod]
        public void Combine()
        {
            var e = new EventEmitter();
            e.On("event", () =>
            {
                Assert.IsTrue(true);
            });
            e.On("event", (bool value) =>
            {
                Assert.IsTrue(value);
            });
            e.Emit("event", true);
        }
    }
}
