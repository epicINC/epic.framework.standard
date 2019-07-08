using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.CIM
{
    public enum LogicalDeviceStatusInfoType : UInt16
    {
        Other = 1,

        Unknown = 2,

        Enabled = 3,

        Disabled = 4,

        NotApplicable = 5,
    }
}
