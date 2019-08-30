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

            var result = await client.PostAsync(new Uri("https://api.exporegist.com/common/api/sms/GetVerificationCode?mobile=18616776117&sign=2019 婚纱及儿童摄影展&source=CWE&token=8b65a32c9aDB61e6"), null);
            var txt = await result.Content.ReadAsStringAsync();

        }
    }
}
