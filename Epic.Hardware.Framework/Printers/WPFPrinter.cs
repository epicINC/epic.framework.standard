using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Epic.Hardware.Printers
{
    /// <summary>
    ///  https://github.com/dotnet/wpf/blob/master/src/Microsoft.DotNet.Wpf/src/PresentationFramework/System/Windows/Controls/PrintDialog.cs
    /// </summary>
    public static class XpsPrinter
    {


        public static bool Dialog(DocumentPaginator document, Action<PrintDialog> setting = null, string printer = null, string description = null)
        {
            var dialog = new PrintDialog();

            if (setting != null) setting(dialog);

            if (printer != null)
                dialog.PrintQueue = QueueHelper.Find(printer);

            // 用户确认
            if (dialog.ShowDialog() != true) return false;

            dialog.PrintDocument(document, description);
            return true;
        }

        public static bool Dialog(XpsDocument document, Action<PrintDialog> setting = null, string printer = null, string description = null)
        {
            // STA
            return Dialog(document.GetFixedDocumentSequence().DocumentPaginator, setting, printer, description);
        }

        public static bool Print(DocumentPaginator document, string printer = null, PrintTicket ticket = null)
        {
            var queue = QueueHelper.FindOrDefault(printer);
            if (queue == null) return false;

            var writer = PrintQueue.CreateXpsDocumentWriter(queue);

            if (ticket != null)
                writer.Write(document, ticket);
            else
                writer.Write(document);

            return true;
        }

        public static bool Print(XpsDocument document, string printer = null, PrintTicket ticket = null)
        {
            // STA
            return Print(document.GetFixedDocumentSequence().DocumentPaginator, printer, ticket);
        }


        public static bool Print(Stream stream, string printer = null)
        {
            var queue = QueueHelper.FindOrDefault(printer);
            if (queue == null) return false;
            QueueHelper.Print(queue, stream);
            return true;
        }


        public static bool Print(string path, string printer = null)
        {
            var queue = QueueHelper.FindOrDefault(printer);
            if (queue == null) return false;
            try
            {
                var job = QueueHelper.Print(queue, path);
                job.Commit();
            }
            catch (Exception e)
            {

                throw;
            }
  
            return true;
        }


        /// <summary>
        /// Fail
        /// </summary>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-3.5/aa969772%28v%3dvs.90%29
        /// </remarks>
        /// <param name="files"></param>
        /// <param name="printer"></param>
        /// <returns></returns>
        public static bool PrintFiles(string[] files, string printer = null)
        {
            var queue = QueueHelper.FindOrDefault(printer);
            if (queue == null) return false;

            try
            {
                FileInfo file;
                foreach (var item in files)
                {
                    file = new FileInfo(item);
                    if (!file.Exists) continue;

                    queue.AddJob(file.Name, file.FullName, false);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }
    }
}
