using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Epic.Hardware.CIM
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/windows/win32/cimwin32prov/cim-logicalelement
    /// </summary>
    public class LogicalElement : ManagedSystemElement
    {
        internal LogicalElement()
        {
        }

        protected LogicalElement(ManagementBaseObject value) : base(value)
        {
        }
    }
}
