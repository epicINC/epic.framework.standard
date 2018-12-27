using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DevTest
{
    public class GZIPTest
    {

        static string original = "id=62015836&n=张钢&c=北京昆仑亿发科技股份有限公司&p=董事长r=1541748819122";
        public static void Test()
        {
            var d = Encoding.UTF8.GetBytes(original);
            Console.WriteLine(d.Length);
            var r = Compress(d);
            Console.WriteLine(r.Length);

        }


        public static byte[] Compress(byte[] value)
        {
            using (var ms = new MemoryStream())
            {
                using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(value, 0, value.Length);
                    zip.Close();
                    return ms.ToArray();
                }
            }
        }
    }
}
