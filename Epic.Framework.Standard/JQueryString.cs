using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace Epic
{
    public static class JQueryString
    {
        static readonly Type AliasType = typeof(Newtonsoft.Json.JsonPropertyAttribute);

        public static NameValueCollection Parse(string value)
        {
            return System.Web.HttpUtility.ParseQueryString(value);
        }

        public static IDictionary<string, object> Parse<T>(T value) where T : class
        {
            var result = new Dictionary<string, object>();
            if (value == null) return result;

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(value))
                result.Add(Alias(item), item.GetValue(value));
                
            return result;
        }

        static string Alias(PropertyDescriptor value)
        {
            if (value.Attributes.Count == 0) return value.Name;
            var attr = value.Attributes[AliasType] as Newtonsoft.Json.JsonPropertyAttribute;
            if (attr == null || String.IsNullOrWhiteSpace(attr.PropertyName)) return value.Name;

            return attr.PropertyName;
        }


        public static string Stirngify(IEnumerable<KeyValuePair<string, object>> value)
        {
            var result = new List<string>();
            foreach(KeyValuePair<string, object> item in value)
            {
                switch(item.Value)
                {
                    case string s:
                        result.Add($"{item.Key}={Uri.EscapeDataString(s)}");
                        continue;
                    case int y:
                    case byte b:
                    case long l:
                        result.Add($"{item.Key}={item.Value}");
                        continue;
                    case bool b:
                        result.Add($"{item.Key}={(b ? 1 : 0)}");
                        continue;
                    case IEnumerable<object> list:
                        if (list.Any())
                            result.Add($"{item.Value} = {String.Join(", ", list)}");
                        continue;
                }

            }
            return String.Join("&", result);
        }


    }
}
