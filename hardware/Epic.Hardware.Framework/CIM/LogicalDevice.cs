using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using Epic.Hardware.Extensions;

namespace Epic.Hardware.CIM
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/windows/win32/cimwin32prov/cim-logicaldevice
    /// </summary>
    public class LogicalDevice : LogicalElement
    {
        internal LogicalDevice()
        {

        }

        protected LogicalDevice(ManagementBaseObject value) : base(value)
        {
            Parse(this, value);
        }

        protected static void Parse(LogicalDevice instance, ManagementBaseObject value)
        {
            instance.Availability = value["Availability"].Cast<LogicalDeviceStatusType>();
            instance.ConfigManagerErrorCode = value["ConfigManagerErrorCode"].Cast<uint>();
            instance.ConfigManagerUserConfig = value["ConfigManagerUserConfig"].Cast<bool>();
            instance.CreationClassName = value["CreationClassName"].TryCast<string>();
            instance.DeviceID = value["DeviceID"].TryCast<string>();
            instance.PowerManagementCapabilities = value["PowerManagementCapabilities"].TryCast<ushort, PowerManagementCapabilityType>();
            instance.ErrorCleared = value["ErrorCleared"].Cast<bool>();
            instance.ErrorDescription = value["ErrorDescription"].TryCast<string>();
            instance.LastErrorCode = value["LastErrorCode"].Cast<uint>();
            instance.PNPDeviceID = value["PNPDeviceID"].TryCast<string>();
            instance.PowerManagementSupported = value["PowerManagementSupported"].Cast<bool>();
            instance.StatusInfo = value["StatusInfo"].Cast<LogicalDeviceStatusInfoType>();
            instance.SystemCreationClassName = value["SystemCreationClassName"].TryCast<string>();
            instance.SystemName = value["SystemName"].TryCast<string>();

        }


        public LogicalDeviceStatusType? Availability { get; protected set; }
        /// <summary>
        /// todo
        /// </summary>
        public uint? ConfigManagerErrorCode { get; protected set; }

        public bool? ConfigManagerUserConfig { get; protected set; }

        public string CreationClassName { get; protected set; }
        public string DeviceID { get; protected set; }
        public bool? ErrorCleared { get; protected set; }
        public string ErrorDescription { get; protected set; }
        public uint? LastErrorCode { get; protected set; }
        public string PNPDeviceID { get; protected set; }
        public PowerManagementCapabilityType[] PowerManagementCapabilities { get; protected set; }
        public bool? PowerManagementSupported { get; protected set; }

        public LogicalDeviceStatusInfoType? StatusInfo { get; protected set; }
        public string SystemCreationClassName { get; protected set; }
        public string SystemName { get; protected set; }

    }
}
