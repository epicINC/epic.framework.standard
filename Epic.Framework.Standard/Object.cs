using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Epic
{
    public class JObject
    {
        public static object DeepClone(object value)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                return formatter.Deserialize(stream);
            }
        }

        public static T DeepClone<T>(T value)
        {
            if (DeepClone((object)value) is T result) return result;
            return default(T);
        }
    }
}
