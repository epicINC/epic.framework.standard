using Epic.Hardware.WMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Epic.Hardware.Printers
{


    public class PrinterHelper
    {


        public static string Default
        {
            get { return new PrintDocument().PrinterSettings.PrinterName; }
        }

        public static bool HasPrinter
        {
            get { return PrinterSettings.InstalledPrinters.Count > 0; }
        }


        /// <summary>   
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。   
        /// </summary>   
        public static IEnumerable<string> Names
        {
            get
            {
                var result = new HashSet<string>();
                result.Add(Default);
                for (var i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    result.Add(PrinterSettings.InstalledPrinters[i]);
                return result;
            }
        }

    }
}
