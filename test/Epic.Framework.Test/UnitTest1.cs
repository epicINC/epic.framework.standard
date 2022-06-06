using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epic.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var promise = Promise.Taskify<bool, Exception>(async (resolve, reject) =>
            {

                await Task.Delay(1000);
                resolve(true);


            });
        }


    }
}
