using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.WMI
{
    [Flags]
    public enum PrinterAttributeType : uint
    {
        Queued = 0x1,
        Direct = 0x2,
        Default = 0x4,
        Shared = 0x8,
        Network = 0x10,
        Hidden = 0x20,
        Local = 0x40,
        EnableDevQ = 0x80,
        KeepPrintedJobs = 0x100,
        DoCompleteFirst = 0x200,
        WorkOffline = 0x400,
        EnableBIDI = 0x800,
        RawOnly = 0x1000,
        Published = 0x2000,
    }
}
