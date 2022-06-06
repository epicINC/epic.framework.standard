﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.WMI
{
    public enum PrinterExtendedDetectedErrorStateType : ushort
    {
        Unknown = 0x0,
        Other = 0x1,
        NoError = 0x2,
        LowPaper = 0x3,
        NoPaper = 0x4,
        LowToner = 0x5,
        NoToner = 0x6,
        DoorOpen = 0x7,
        Jammed = 0x8,
        ServiceRequested = 0x9,
        OutputBinFull = 0xA,
        PaperProblem = 0xB,
        CannotPrintPage = 0xC,
        UserInterventionRequired = 0xD,
        OutofMemory = 0xE,
        ServerUnknown = 0xF,
    }
}
