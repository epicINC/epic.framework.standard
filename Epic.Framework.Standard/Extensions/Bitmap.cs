using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
    public static class BitmapExtensions
    {
        public static async Task<T> ToStreamAsync<T>(this Bitmap value, Func<Stream, Task<T>> action)
        {
            using (var ms = new MemoryStream())
            {
                value.Save(ms, ImageFormat.Jpeg);
                ms.Position = 0;
                return await action(ms);
            }
        }

    }
}
