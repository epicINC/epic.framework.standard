using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.CIM
{
    public enum PrinterDetectedErrorStateType : ushort
    {
        Unknown = 0,
        Other = 1,
        NoError = 2,
        LowPaper = 3,
        NoPaper = 4,
        LowToner = 5,
        NoToner = 6,
        DoorOpen = 7,
        Jammed = 8,
        Offline = 9,
        ServiceRequested = 10,
        OutputBinFull = 11
    }
}
