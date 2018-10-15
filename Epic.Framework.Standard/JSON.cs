using System;
using System.IO;
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

        public static object Parse(Stream value, Type objectType)
        {
            using (var sr = new StreamReader(value))
            {
                return (new JsonSerializer()).Deserialize(sr, objectType);
            }
        }

        public static object Parse(byte[] value, Type objectType)
        {
            using (var ms = new MemoryStream(value))
            {
                return Parse(ms, objectType);
            }
        }

        public static T Parse<T>(byte[] value)
        {
            return (T)Parse(value, typeof(T));
        }

        public static T Parse<T>(Stream value)
        {
            return (T)Parse(value, typeof(T));
        }


        public static T Read<T>(string path)
        {
            if (!File.Exists(path)) return default(T);

            using (var stream = File.OpenRead(path))
            {
                return Parse<T>(stream);
            }
        }

        public static bool Save(string path, object value)
        {
            try
            {
                using (var sw = File.CreateText(path))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(sw, value);
                }
                return true;
            } catch
            {
                return false;
            }

        }

    }
}
