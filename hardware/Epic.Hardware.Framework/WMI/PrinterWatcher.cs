using Epic.Hardware.CIM;
using Epic.Hardware.Printers;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Linq;
using Epic.Hardware.Extensions;

namespace Epic.Hardware.WMI
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc3805
    /// https://docs.microsoft.com/zh-cn/windows/desktop/CIMWin32Prov/win32-printer
    /// https://docs.microsoft.com/en-us/windows/desktop/wmisdk/--instancemodificationevents
    /// </summary>
    public class PrinterWatcher : Watcher
    {

        public static PrinterWatcher Default
        {
            get { return new PrinterWatcher(PrinterHelper.Default); }
        }



        public PrinterWatcher(string name = null, bool autoStart = false)
        {
            this.Query = $"SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA \"Win32_Printer\"{(String.IsNullOrWhiteSpace(name) ? String.Empty : $" AND TargetInstance.Name=\"{name}\"")}";
            if (this.AutoStart = autoStart)
                this.Start();
        }

        public event Action<PrinterWatcher, Win32Printer> Initializing;
        public event Action<PrinterWatcher, Win32Printer> Idle;
        public event Action<PrinterWatcher, Win32Printer> Printing;
        public event Action<PrinterWatcher, Win32Printer> Printed;
        public event Action<PrinterWatcher, Win32Printer> Error;

        Win32Printer Args { get; set; } = new Win32Printer();

        protected override void OnChanged(ManagementBaseObject e)
        {
            base.OnChanged(e);
            this.OnChanged(new Win32Printer(e));
        }

        void OnChanged(Win32Printer e)
        {
            if (e == null) return;
            //if (e.PrinterStatus == this.Args.PrinterStatus) return;

            switch (e.PrinterStatus)
            {
                case PrinterStatusType.Printing:
                    this.OnPrinting(e);
                    break;
                case PrinterStatusType.Idle:
                    if (this.Args.PrinterStatus == PrinterStatusType.Printing)
                        this.OnPrinted(e);
                    this.OnIdle(e);
                    break;
            }

            this.OnError(e);
            this.Args = e;
        }


        void OnInitializing(Win32Printer e)
        {
            if (this.Initializing == null) return;
            this.Initializing(this, e);
        }

        void OnIdle(Win32Printer e)
        {
            if (this.Idle == null) return;
            this.Idle(this, e);
        }

        void OnPrinting(Win32Printer e)
        {
            if (this.Printing == null) return;
            this.Printing(this, e);
        }

        void OnPrinted(Win32Printer e)
        {
            if (this.Printed == null) return;
            this.Printed(this, e);
        }

        void OnError(Win32Printer e)
        {
            if (this.Error == null) return;
            if (e.DetectedErrorState == PrinterDetectedErrorStateType.NoError) return;
            this.Error(this, e);
        }



        public static bool WaitForPrinted(string name)
        {
            var query = $"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_Printer'{(String.IsNullOrWhiteSpace(name) ? String.Empty : $" AND TargetInstance.Name = '{name}' AND (TargetInstance.PrinterStatus=4 OR TargetInstance.PrinterStatus=3)")}";
            var previous = PrinterStatusType.Unknown;
            PrinterStatusType? status;
            return Watcher.WaitForEvent(query, 20, e => {
                status = (e?.Properties["TargetInstance"]?.Value as ManagementBaseObject)?["PrinterStatus"].Cast<PrinterStatusType>();
                if (!status.HasValue) return false;
                if (previous == PrinterStatusType.Printing && status == PrinterStatusType.Idle) return true;
                previous = status.Value;
                return false;
            }) != null;

           //var previous = PrinterStatusType.Unknown;
           // PrinterStatusType? status;
           // try
           // {
           //     using (var watcher = new ManagementEventWatcher(query))
           //     {
           //         watcher.Options.Timeout = TimeSpan.FromSeconds(20);
           //         while (true)
           //         {
           //             status = (watcher.WaitForNextEvent()?.Properties["TargetInstance"]?.Value as ManagementBaseObject).Cast<PrinterStatusType>();
           //             if (!status.HasValue) return false;
           //             if (previous == PrinterStatusType.Printing && status == PrinterStatusType.Idle) return true;

           //             previous = status.Value;
           //         }
           //     }

           // }
           // catch
           // {
           //     return false;
           // }

            //ManagementBaseObject item;
            //while (true)
            //{
            //    item = watcher.WaitForNextEvent();
            //    var status = (PrinterStatusType)item["PrinterStatus"];
            //    Console.WriteLine(status);
            //}
        }

        public enum PrinterEventType  : uint
        {
            Printed = 3

        }
    }
}
