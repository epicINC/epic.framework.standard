using Epic.Tips;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Test.Tips
{
    [TestClass]
    public class StructTest
    {
        [TestMethod]
        public void FloatBytes()
        {
            var result = new FloatBytes();
            result.Value = 0.4f;
            Assert.AreEqual(result.Index0, 205);
            Assert.AreEqual(result.Index1, 204);
            Assert.AreEqual(result.Index2, 204);
            Assert.AreEqual(result.Index3, 62);
        }
    }
}
