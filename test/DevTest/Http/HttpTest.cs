using System;
using System.Collections.Generic;
using System.Text;
using Epic.Extensions;

namespace DevTest
{
    class HttpTest
    {
        public static async void Test()
        {
            var url = "http://emaster.eastfair.com/organizer.visitor.provider/login/index";

            var client = new System.Net.Http.HttpClient();
            var result = await client.PostFormAsync(url, new { userName = "admin", password = "123456" });


        }
    }
}
