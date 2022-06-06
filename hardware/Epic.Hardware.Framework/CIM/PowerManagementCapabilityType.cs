using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.CIM
{
    public enum PowerManagementCapabilityType : ushort
    {
        Unknown = 0,
        NotSupported = 1,
        Disabled = 2,
        Enabled = 3,
        PowerSavingModesEnteredAutomatically = 4,
        PowerStateSettable = 5,
        PowerCyclingSupported = 6,
        TimedPowerOnSupported = 7,s
    }
}
