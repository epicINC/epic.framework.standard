using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Packaging;

namespace Epic.Hardware.Printers
{
    public partial class QueueHelper
    {
        /// <summary>
        /// Print
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PrintSystemJobInfo Print(PrintQueue queue, Stream value)
        {
            if (queue == null) return null;

            var job = queue.AddJob();
            using (var stream = job.JobStream)
                value.CopyTo(stream);
            return job;
        }

        public static PrintSystemJobInfo Print(PrintQueue queue, string name, string path)
        {
            if (!File.Exists(path)) return null;
            var result = queue.AddJob(name, path, false);
            queue.Commit();
            return result;
        }

        public static PrintSystemJobInfo Print(PrintQueue queue, string path)
        {
            return Print(queue, System.IO.Path.GetFileName(path), path);
        }

        public static PrintQueue Default
        {
            get => LocalPrintServer.GetDefaultPrintQueue();
        }

        public static PrintQueue Find(PrintServer server, string printerQueueName)
        {
            try
            {
                return server.GetPrintQueue(printerQueueName);
            }
            catch
            {
                return null;
            }
        }

        public static PrintQueue Find(string path, string printerQueueName)
        {
            return Find(new PrintServer(path), printerQueueName);
        }

        public static PrintQueue Find(string printerQueueName)
        {
            return Find(new PrintServer(), printerQueueName);
        }


        public static PrintQueue FindOrDefault(string printer)
        {
            if (String.IsNullOrWhiteSpace(printer)) return Default;
            return Find(printer);
        }





    }
}
