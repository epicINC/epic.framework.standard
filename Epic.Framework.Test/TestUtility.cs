using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Test
{
    public static class EnumerableAssert
    {
        public static void AreEqual<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Assert.AreEqual(actual.Count(), expected.Count());

            var el = actual.GetEnumerator();
            var er = expected.GetEnumerator();

            while (el.MoveNext() && er.MoveNext())
                Assert.AreEqual(el.Current, er.Current);
        }
    }
}
