using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Hardware.Printers
{
    public static class ProcessPrinter
    {
        static bool Init(string verb, string path, string printer = null)
        {
            if (!File.Exists(path)) return false;
            Process.Start(new ProcessStartInfo(path, printer ?? $"\"{ printer }\"")
            {
                Verb = verb,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            });

            return true;
        }



        public static bool Print(string path, string printer = null)
        {
            return Init("Print", path, printer);
        }


        public static bool PrintTo(string path, string printer = null)
        {
            return Init("PrintTo", path, printer);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/lpr
        /// </remarks>
        /// <returns></returns>
        public static bool LPR(string path, string server = null, string printer = null)
        {
            if (!Path.IsPathRooted(path))
                path = Path.GetFullPath(path);

            var args = String.Format("LPR {0} {1} {2}", server != null ? $" -S {server}" : String.Empty, printer !=null ? $"-P {printer}" : String.Empty, $" {path}");

            Process.Start(args);
            return true;
        }


    }
}
