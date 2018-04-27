using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

namespace Epic
{
    public static class JQueryString
    {
        public static NameValueCollection Parse(string value)
        {
            return System.Web.HttpUtility.ParseQueryString(value);
        }

        public static string Stirngify(IDictionary<string, object> value)
        {
            var result = new List<string>();
            foreach(KeyValuePair<string, object> item in value)
            {

                switch(item.Value)
                {
                    case null:
                        continue;
                    case IEnumerable<object> list:
                        if (list.Any())
                            result.Add($"{item.Value} = {String.Join(", ", list)}");
                        continue;
                }

            }

            return null;
        }


    }
}
