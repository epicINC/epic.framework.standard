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
            var promise = Promise.Promiseify<bool, Exception>(async (resolve, reject) =>
            {

                await Task.Delay(1000);
                resolve(true);


            });

            if (promise.Timeout(2000))
            {
                return;
            }
            var result = await promise.Task;
            return;
        }


    }
}
