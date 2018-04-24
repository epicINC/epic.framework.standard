using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Epic
{
    public static class JSON
    {
        public static object Parse(string value)
        {
            return JsonConvert.DeserializeObject(value);
        }

        public static T Parse<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }


        public static string Stringify(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

    }
}
