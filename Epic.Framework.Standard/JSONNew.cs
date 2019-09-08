using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Epic.IO;
using System.Threading.Tasks;

namespace Epic
{
    /*
    public static class JSONNew
    {

        public static T Parse<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }


        public static T Parse<T>(Stream value)
        {
            return ParseAsync<T>(value).Result;
        }


        public static async Task<T> ParseAsync<T>(Stream value)
        {
            return await JsonSerializer.DeserializeAsync<T>(value);
        }

        public static T Parse<T>(byte[] value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        public static string Stringify<T>(T value)
        {
            return JsonSerializer.Serialize<T>(value);
        }




        public static T Read<T>(string path)
        {
            return ReadAsync<T>(path).Result;
        }

        public static async Task<T> ReadAsync<T>(string path)
        {
            if (!File.Exists(path)) return default;

            using (var stream = File.OpenRead(path))
            {
                return await ParseAsync<T>(stream);
            }
        }


        public static void Write<T>(Stream stream, T value)
        {
            using (var writer = new Utf8JsonWriter(stream))
            {
                JsonSerializer.Serialize<T>(writer, value);
            }
        }

        public static async Task WriteAsync<T>(Stream stream, T value)
        {
            await JsonSerializer.SerializeAsync<T>(stream, value);
        }

        public static bool Save<T>(string path, T value)
        {
            return SaveAsync<T>(path, value).Result;
        }

        public static async Task<bool> SaveAsync<T>(string path, T value)
        {
            try
            {
                using (var sw = File.Create(path))
                {
                    await WriteAsync<T>(sw, value);
                }
                return true;
            } catch
            {
                return false;
            }

        }

    }
    */
}
