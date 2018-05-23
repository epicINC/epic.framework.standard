using Epic.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Test.Components
{
    [TestClass]
    public class ProbabilityControlTest
    {
        [TestMethod]
        public void Repeat()
        {
            var pc = ProbabilityControl.Fix(new double[] { 0.1,0.2 });


            Assert.IsTrue(pc.Calc(0.1) >= 0);

        }

    }
}
