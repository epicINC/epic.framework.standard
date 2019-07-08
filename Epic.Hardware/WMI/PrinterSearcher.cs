using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Linq;

namespace Epic.Hardware.WMI
{
    public class PrinterSearcher
    {
        public static IEnumerable<ManagementBaseObject> Query(string name = null)
        {
            var query = $"SELECT * FROM Win32_Printer{(String.IsNullOrEmpty(name) ? String.Empty : $" WHERE Name LIKE '%{name}%'")}";
            return WMISearcher.Query(query);
        }

        public static Win32Printer FindByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return default;
            return Cast(WMISearcher.Find($"SELECT * FROM Win32_Printer WHERE Name = '{name}'"));
        }


        static IEnumerable<Win32Printer> Cast(IEnumerable<ManagementBaseObject> value)
        {
            return value.Select(Cast);
        }

        static Win32Printer Cast(ManagementBaseObject value)
        {
            if (value == null) return default;
            return new Win32Printer(value); ;
        }
    }
}
