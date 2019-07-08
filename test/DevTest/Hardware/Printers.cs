using Epic.Hardware.Printers;
using Epic.Hardware.WMI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTest.Hardware
{
    public static class Printers
    {
        public static void Test()
        {
            var name = Epic.Hardware.Printers.PrinterHelper.Default;

            var status = Epic.Hardware.Printers.PrinterHelper.Find(name);
            Console.WriteLine(status);
        }



        public static void EventWather()
        {
            var name = PrinterHelper.Default;
            //var query = $"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_Printer'{(String.IsNullOrWhiteSpace(name) ? String.Empty : $" AND TargetInstance.Name = '{name}'")}";

            var result = PrinterWatcher.WaitForPrinted(name);
            Console.WriteLine(result);
        }

        public static void StatusWatcher()
        {
            var watcher = PrinterWatcher.Default;
            watcher.Initializing += Watcher_Initializing;
            watcher.Idle += Watcher_Idle;
            watcher.Printing += Watcher_Printing;
            watcher.Printed += Watcher_Printed;
            watcher.Start();
        }

        private static void Watcher_Printed(PrinterWatcher arg1, Win32Printer arg2)
        {
            Console.WriteLine("Printed");
        }

        private static void Watcher_Printing(PrinterWatcher arg1, Win32Printer arg2)
        {
            Console.WriteLine("Printing");
        }

        private static void Watcher_Initializing(PrinterWatcher arg1, Win32Printer arg2)
        {
            Console.WriteLine("Initializing");
        }

        private static void Watcher_Idle(PrinterWatcher arg1, Win32Printer arg2)
        {
            Console.WriteLine("Idle");
        }
    }
}
