using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using Epic.Hardware.Extensions;

namespace Epic.Hardware.CIM
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/windows/win32/cimwin32prov/cim-managedsystemelement
    /// </summary>
    public class ManagedSystemElement
    {
        internal ManagedSystemElement()
        {

        }

        protected ManagedSystemElement(ManagementBaseObject value)
        {
            Parse(this, value);
        }

        protected static void Parse(ManagedSystemElement instance, ManagementBaseObject value)
        {
            instance.Caption = value["Caption"].TryCast<string>();
            instance.Description = value["Description"].TryCast<string>();
            instance.InstallDate = value["InstallDate"].Cast<DateTime>();
            instance.InstallDate = value["InstallDate"].Cast<DateTime>();
            instance.Name = value["Name"].TryCast<string>();
            instance.Status = value["Status"].TryCast<string>();
        }

        public string Caption { get; protected set; }
        public string Description { get; protected set; }
        public DateTime? InstallDate { get; protected set; }
        public string Name { get; protected set; }
        public string Status { get; protected set; }
    };
}
