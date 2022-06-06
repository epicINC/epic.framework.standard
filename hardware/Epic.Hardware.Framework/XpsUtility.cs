using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using Image = System.Drawing.Image;

namespace Epic.Hardware
{
    public class XpsUtility
    {

        /// <summary>
        /// XPS Stream To FixedDocument
        /// STA
        /// ref
        /// http://geekswithblogs.net/shahed/archive/2007/09/22/115540.aspx
        /// https://stackoverflow.com/questions/6767192/c-sharp-open-office-documents-and-xps-files-from-memorystream
        /// </summary>
        /// <param name="stream"></param>
        public static FixedDocumentSequence Convert(Stream stream)
        {
            var package = Package.Open(stream);
            var name = String.Format("memorystream://{0}.xps", Guid.NewGuid());
            var uri = new Uri(name);

            PackageStore.AddPackage(uri, package);

            var doc = new XpsDocument(package, CompressionOption.Maximum, name);
            return doc.GetFixedDocumentSequence();
        }


        public static List<Image> ToBitmap(XpsDocument document)
        {
            var result = new List<Image>();

            var sequence = document.GetFixedDocumentSequence();

            RenderTargetBitmap render = null;
            var encoder = new BmpBitmapEncoder();
            for (int i = 0; i < sequence.DocumentPaginator.PageCount; i++)
                result.Add(ToBitmap(sequence.DocumentPaginator.GetPage(i), render, encoder));

            return result;
        }

        static Image ToBitmap(XpsDocument document, int index, RenderTargetBitmap render, BmpBitmapEncoder encoder)
        {
            return ToBitmap(document.GetFixedDocumentSequence(), index, render, encoder);
        }

        public static Image ToBitmap(FixedDocumentSequence document, int index, RenderTargetBitmap render, BmpBitmapEncoder encoder)
        {
            return ToBitmap(document.DocumentPaginator.GetPage(index), render, encoder);
        }


        static Image ToBitmap(DocumentPage document, RenderTargetBitmap render, BmpBitmapEncoder encoder)
        {
            if (render == null)
                render = new RenderTargetBitmap((int)document.Size.Width, (int)document.Size.Height, 96, 96, System.Windows.Media.PixelFormats.Default);
            if (encoder == null)
                encoder = new BmpBitmapEncoder();

            render.Render(document.Visual);
            encoder.Frames.Add(BitmapFrame.Create(render));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                ms.Flush();
                return Bitmap.FromStream(ms);
            }
        }



    }



}
