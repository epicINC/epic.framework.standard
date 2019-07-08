using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.WMI
{
    public enum PrinterStateType : uint
    {
        Idle = 0,
        Paused = 1,
        Error = 2,
        PendingDeletion = 3,
        PaperJam = 4,
        PaperOut = 5,
        ManualFeed = 6,
        PaperProblem = 7,
        Offline = 8,
        IOActive = 9,
        Busy = 10,
        Printing = 11,
        OutputBinFull = 12,
        NotAvailable = 13,
        Waiting = 14,
        Processing = 15,
        Initialization = 16,
        WarmingUp = 17,
        TonerLow = 18,
        NoToner = 19,
        PagePunt = 20,
        UserInterventionRequired = 21,
        OutofMemory = 22,
        DoorOpen = 23,
        ServerUnknown = 24,
        PowerSave = 25,



        LidOpen = 4194432,
        Outofpaper = 144,
        OutofpaperAndLidopen = 4194448,
        CustomPrinting = 1024,
        Initializing = 32768,
        ManualFeedinProgress = 160,
        CustomOffline = 4096,
    }
}
