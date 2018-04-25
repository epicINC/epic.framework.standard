using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.Win32
{
    public class API
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static unsafe extern void CopyMemory(void* dest, void* src, int count);

    }
}
