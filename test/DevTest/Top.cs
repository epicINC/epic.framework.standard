using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DevTest
{
    class Top
    {

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);



        void Test()
        {
            //var form = Control.FromHandle(handle);
            //if (form == null) continue;

            //SetWindowPos(handle, HWND_TOPMOST, 0, 0, form.Bounds.Width, form.Bounds.Height, SWP_NOZORDER | SWP_SHOWWINDOW);
            //SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, form.Bounds.Width, form.Bounds.Height, SWP_NOZORDER | SWP_SHOWWINDOW);

        }




    }
}
