using System;
using System.Collections.Generic;
using System.Text;
using Epic.Extensions;

namespace DevTest
{
    class HttpTest
    {
        public static readonly Uri Login = new Uri("/organizer.visitor.provider/login/index", UriKind.Relative);
        public static async void Test()
        {
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("http://emaster.eastfair.com");

            var result = await client.PostFormAsync(Login, new { userName = "admin", password = "123456" });


        }
    }
}
