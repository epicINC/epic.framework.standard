using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Epic.Hardware.Printing
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/framework/wpf/advanced/how-to-programmatically-print-xps-files
    /// </summary>
    public class BatchXPSPrint
    {

        PrintQueue Queue;
        bool IsXpsDevice;

        public BatchXPSPrint(PrintQueue queue)
        {
            this.Queue = queue;
            this.IsXpsDevice = queue.IsXpsDevice;
        }


        void Add(string file)
        {
            this.Queue.AddJob(Path.GetFileNameWithoutExtension(file), file, this.IsXpsDevice);

        }
    }
}
