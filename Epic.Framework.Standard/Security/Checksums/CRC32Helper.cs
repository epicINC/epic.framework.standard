using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Epic.Security.Checksums
{
    public class CRC32Helper
    {
        public static long CRC32(byte[] value)
        {
            var crc = new CRC32();
            crc.Update(value);
            return crc.Value;
        }

        public static long CRC32FromString(string value)
        {
            return CRC32(Encoding.Default.GetBytes(value));
        }

        public static long CRC32FromFile(string value)
        {
            var fs = new FileStream(value, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            return CRC32(bytes);
        }
    }
}
