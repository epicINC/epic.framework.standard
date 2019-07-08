using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Drawing.Printing;
using Epic.Hardware.WMI;

namespace Epic.Hardware.Printers
{
    public class LocalPrinter
    {
        public LocalPrinter(string name)
        {
            this.Name = name;

        }

        public string Name { get; set; }


 
    }
}
