using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Linq;
using Epic.Hardware.Extensions;

namespace Epic.Hardware.WMI
{
    /// <summary>
    /// 
    /// </summary>
    public class Win32Printer : CIM.Printer
    {
        public static Win32Printer Find(string name)
        {
            return PrinterSearcher.FindByName(name);
        }



        internal Win32Printer()
        {

        }


        internal Win32Printer(ManagementBaseObject value) : base(value)
        {
            Parse(this, value);
        }

        public static void Parse(Win32Printer instance, ManagementBaseObject value)
        {
            instance.Attributes = value["Attributes"].Cast<PrinterAttributeType>();
            instance.AveragePagesPerMinute = value["AveragePagesPerMinute"].Cast<uint>();
            instance.Comment = value["Comment"].TryCast<string>();
            instance.Default = value["Default"].Cast<bool>();
            instance.DefaultPriority = value["DefaultPriority"].Cast<uint>();
            instance.Direct = value["Direct"].Cast<bool>();
            instance.DoCompleteFirst = value["DoCompleteFirst"].Cast<bool>();
            instance.DriverName = value["DriverName"].TryCast<string>();
            instance.EnableBIDI = value["EnableBIDI"].Cast<bool>();
            instance.EnableDevQueryPrint = value["EnableDevQueryPrint"].Cast<bool>();
            instance.ExtendedDetectedErrorState = value["ExtendedDetectedErrorState"].Cast<PrinterExtendedDetectedErrorStateType>();
            instance.ExtendedPrinterStatus = value["ExtendedPrinterStatus"].Cast<PrinterExtendedPrinterStatusType>();
            instance.Hidden = value["Hidden"].Cast<bool>();
            instance.KeepPrintedJobs = value["KeepPrintedJobs"].Cast<bool>();
            instance.Local = value["Local"].Cast<bool>();
            instance.Location = value["Location"].TryCast<string>();
            instance.Network = value["Network"].Cast<bool>();
            instance.Parameters = value["Parameters"].TryCast<string>();
            instance.PortName = value["PortName"].TryCast<string>();
            instance.PrinterPaperNames = value["PrinterPaperNames"].TryCast<string[]>();
            instance.PrinterState = (PrinterStateType)value["PrinterState"];
            instance.PrintJobDataType = value["PrintJobDataType"].TryCast<string>();
            instance.PrintProcessor = value["PrintProcessor"].TryCast<string>();
            instance.Priority = value["Priority"].Cast<uint>();
            instance.Published = value["Published"].Cast<bool>();
            instance.Queued = value["Queued"].Cast<bool>();
            instance.RawOnly = value["RawOnly"].Cast<bool>();
            instance.SeparatorFile = value["SeparatorFile"].TryCast<string>();
            instance.ServerName = value["ServerName"].TryCast<string>();
            instance.Shared = value["Shared"].Cast<bool>();
            instance.ShareName = value["ShareName"].TryCast<string>();
            instance.SpoolEnabled = value["SpoolEnabled"].Cast<bool>();
            instance.StartTime = value["StartTime"].Cast<DateTime>();
            instance.UntilTime = value["UntilTime"].Cast<DateTime>();
            instance.WorkOffline = value["WorkOffline"].Cast<bool>();
        }

        public PrinterAttributeType? Attributes { get; protected set; }
        public uint? AveragePagesPerMinute { get; protected set; }
        public string Comment { get; protected set; }
        public bool? Default { get; protected set; }
        public uint? DefaultPriority { get; protected set; }
        public bool? Direct { get; protected set; }
        public bool? DoCompleteFirst { get; protected set; }
        public string DriverName { get; protected set; }
        public bool? EnableBIDI { get; protected set; }
        public bool? EnableDevQueryPrint { get; protected set; }
        public PrinterExtendedDetectedErrorStateType? ExtendedDetectedErrorState { get; protected set; }
        public PrinterExtendedPrinterStatusType? ExtendedPrinterStatus { get; protected set; }
        public bool? Hidden { get; protected set; }
        public bool? KeepPrintedJobs { get; protected set; }
        public bool? Local { get; protected set; }
        public string Location { get; protected set; }
        public bool? Network { get; protected set; }
        public string Parameters { get; protected set; }
        public string PortName { get; protected set; }
        public string[] PrinterPaperNames { get; protected set; }
        public PrinterStateType? PrinterState { get; protected set; }
        public string PrintJobDataType { get; protected set; }
        public string PrintProcessor { get; protected set; }
        public uint? Priority { get; protected set; }
        public bool? Published { get; protected set; }
        public bool? Queued { get; protected set; }
        public bool? RawOnly { get; protected set; }
        public string SeparatorFile { get; protected set; }
        public string ServerName { get; protected set; }
        public bool? Shared { get; protected set; }
        public string ShareName { get; protected set; }
        public bool? SpoolEnabled { get; protected set; }
        public DateTime? StartTime { get; protected set; }
        public DateTime? UntilTime { get; protected set; }
        public bool? WorkOffline { get; protected set; }
    }

}
