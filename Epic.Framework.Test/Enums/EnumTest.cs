using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Enums;
using Epic.Enums.Extensions;

namespace Epic.Framework.Test.Enums
{
    [TestClass]
    public class EnumTest
    {
        [Flags]
        public enum Op
        {
            Insert = 1 << 0,
            Update = 1 << 1,
            Delete = 1 << 2,
            All = Insert | Update | Delete

        }

        public struct TestS
        {

        }

        [TestMethod]
        public void Normal()
        {
            var type = typeof(TestS);
        }

        [TestMethod]
        public void And()
        {
            Assert.IsTrue(Op.All.HasValue(2));
        }
    }
}
