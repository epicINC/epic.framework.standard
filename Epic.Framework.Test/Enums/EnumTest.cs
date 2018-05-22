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
    /*
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

        [TestMethod]
        public void And()
        {
            Assert.IsTrue(Op.All.And(0b0010) == Op.Update);
            Assert.IsTrue(Op.All.And(Op.Update) == Op.Update);
        }

        [TestMethod]
        public void Or()
        {
            Assert.IsTrue(Op.All.And(0b0010) == Op.Update);
            Assert.IsTrue(Op.All.And(Op.Update) == Op.Update);
        }

        [TestMethod]
        public void Xor()
        {
            Assert.IsTrue(Op.All.Xor(0b0010) == (Op.Insert | Op.Delete));
            Assert.IsTrue(Op.All.Xor(Op.Update) == (Op.Insert | Op.Delete));
        }

        [TestMethod]
        public void Not()
        {
            Assert.IsTrue(Op.All.Not() == (Op)(-8));
        }


        [TestMethod]
        public void Set()
        {
            // not exists(add)
            Assert.IsTrue(Op.Insert.Set(0b0010) == (Op.Insert | Op.Update));
            Assert.IsTrue(Op.Insert.Set(Op.Update) == (Op.Insert | Op.Update));

            // exists(remove)
            Assert.IsTrue(Op.All.Set(0b0010) == (Op.Insert | Op.Delete));
            Assert.IsTrue(Op.All.Set(Op.Update) == (Op.Insert | Op.Delete));
        }


        [TestMethod]
        public void Add()
        {
            // not exists(add)
            Assert.IsTrue(Op.Insert.Add(0b0010) == (Op.Insert | Op.Update));
            Assert.IsTrue(Op.Insert.Add(Op.Update) == (Op.Insert | Op.Update));

            // exists(not change)
            Assert.IsTrue(Op.All.Add(0b0010) == Op.All);
            Assert.IsTrue(Op.All.Add(Op.Update) == Op.All);
        }


        [TestMethod]
        public void Remove()
        {
            // not exists(not change)
            Assert.IsTrue(Op.Insert.Remove(0b0010) == Op.Insert);
            Assert.IsTrue(Op.Insert.Remove(Op.Update) == Op.Insert);

            // exists(remove)
            Assert.IsTrue(Op.All.Remove(0b0010) == (Op.Insert | Op.Delete));
            Assert.IsTrue(Op.All.Remove(Op.Update) == (Op.Insert | Op.Delete));
        }

        [TestMethod]
        public void HasValue()
        {
            // not contain value
            Assert.IsFalse(Op.Insert.HasValue(0b0010));
            Assert.IsFalse(Op.Insert.HasValue(Op.Update));

            // contain
            Assert.IsTrue(Op.All.HasValue(0b0010));
            Assert.IsTrue(Op.All.HasValue(Op.Update));

        }

    }
    */
}
