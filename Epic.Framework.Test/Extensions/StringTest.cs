using System;
using Epic.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epic.Framework.Test
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void Repeat()
        {
            Assert.AreEqual("abc".Repeat(-1), String.Empty);
            Assert.AreEqual("abc".Repeat(0), String.Empty);
            Assert.AreEqual("abc".Repeat(1), "abc");
            Assert.AreEqual("abc".Repeat(2), "abcabc");
        }

        [TestMethod]
        public void PadLeft()
        {
            Assert.AreEqual("abc".PadLeft(10), "       abc");
            Assert.AreEqual("abc".PadLeft(10, "foo"), "foofoofabc");
            Assert.AreEqual("abc".PadLeft(6, "123465"), "123abc");
            Assert.AreEqual("abc".PadLeft(8, "0"), "00000abc");
            Assert.AreEqual("abc".PadLeft(1), "abc");
        }

        [TestMethod]
        public void PadRight()
        {
            Assert.AreEqual("abc".PadRight(10), "abc       ");
            Assert.AreEqual("abc".PadRight(10, "foo"), "abcfoofoof");
            Assert.AreEqual("abc".PadRight(6, "123465"), "abc123");
            Assert.AreEqual("abc".PadRight(8, "0"), "abc00000");
            Assert.AreEqual("abc".PadRight(1), "abc");
        }
    }
}
