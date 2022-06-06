using System;
using System.Drawing.Printing;

namespace Epic.Hardware.Printers
{
    public partial class Printer
    {


        public static PrintDocument Init(PrintDocument document, string printer, PrintController controller = null, PrinterSettings setting = null, Action onComplated = null)
        {

            if (controller != null) document.PrintController = controller ;
            if (setting != null) document.PrinterSettings = setting;
            if (printer != null) document.PrinterSettings.PrinterName = printer;

            if (onComplated != null)
            {
                if (document.PrintController.IsPreview) return document;
                document.EndPrint += (object sender, PrintEventArgs e) => onComplated();
            }

            return document;
        }

        public static void Print(PrintDocument document, string printer, PrintController controller = null, PrinterSettings setting = null, Action onComplated = null)
        {
            Init(document, printer, controller ?? new StandardPrintController(), setting, onComplated).Print();
        }





    }
}
