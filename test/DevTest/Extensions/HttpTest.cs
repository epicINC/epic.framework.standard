using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Epic.Extensions;

namespace DevTest.Extensions
{
    public class HttpTest
    {
        public class User
        {
            public int ID { get; set; }
        }

        public static async void Test()
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://192.168.3.53").JsonResult<User>();
        }
    }
}
