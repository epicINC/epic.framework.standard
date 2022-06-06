﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Hardware.Printers
{

    [StructLayout(LayoutKind.Sequential)]
	internal struct DOCINFO
    {
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataType;
    }



	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// https://www.c-sharpcorner.com/article/printing-directly-to-the-printer/
	/// </remarks>
	internal class PrintDirect
	{
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
		public static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
		public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern long StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern long WritePrinter(IntPtr hPrinter, string data, int buf, ref int pcWritten);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern long EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern long EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern long ClosePrinter(IntPtr hPrinter);
	}


	internal class Win32Printer
    {

		public static void Print()
        {
			System.IntPtr lhPrinter = new System.IntPtr();

			DOCINFO di = new DOCINFO();
			int pcWritten = 0;
			string st1;


			// text to print with a form feed character	
			st1 = "This is an example of printing directly to a printer\f";
			di.pDocName = "my test document";
			di.pDataType = "RAW";

			// the \x1b means an ascii escape character
			st1 = "\x1b*c600a6b0P\f";
			//lhPrinter contains the handle for the printer opened
			//If lhPrinter is 0 then an error has occured
			PrintDirect.OpenPrinter("\\\\192.168.1.101\\hpl", ref lhPrinter, 0);
			PrintDirect.StartDocPrinter(lhPrinter, 1, ref di);
			PrintDirect.StartPagePrinter(lhPrinter);

			try
			{
				// Moves the cursor 900 dots (3 inches at 300 dpi) in from the left margin, and
				// 600 dots (2 inches at 300 dpi) down from the top margin.
				st1 = "\x1b*p900x600Y";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Using the print model commands for rectangle dimensions, "600a" specifies a rectangle
				// with a horizontal size or width of 600 dots, and "6b" specifies a vertical
				// size or height of 6 dots. The 0P selects the solid black rectangular area fill.
				st1 = "\x1b*c600a6b0P";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Specifies a rectangle with width of 6 dots, height of 600 dots, and a
				// fill pattern of solid black.
				st1 = "\x1b*c6a600b0P";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Moves the current cursor position to 900 dots, from the left margin and
				// 1200 dots down from the top margin.
				st1 = "\x1b*p900x1200Y";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Specifies a rectangle with a width of 606 dots, a height of 6 dots and a
				// fill pattern of solid black.
				st1 = "\x1b*c606a6b0P";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Moves the current cursor position to 1500 dots from the left margin and
				// 600 dots down from the top margin.
				st1 = "\x1b*p1500x600Y";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Specifies a rectangle with a width of 6 dots, a height of 600 dots and a
				// fill pattern of solid black.
				st1 = "\x1b*c6a600b0P";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

				// Send a form feed character to the printer
				st1 = "\f";
				PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			PrintDirect.EndPagePrinter(lhPrinter);
			PrintDirect.EndDocPrinter(lhPrinter);
			PrintDirect.ClosePrinter(lhPrinter);
		}
    }
}
