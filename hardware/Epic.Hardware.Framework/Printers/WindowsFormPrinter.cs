using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epic.Hardware.Printers
{
    public static class WindowsFormPrinter
    {

        public static bool Dialog(PrintDocument document, Action<PrintDialog> config = null, string printer = null, PrintController controller = null, PrinterSettings setting = null, Action onComplated = null)
        {
            Printer.Init(document, printer, controller, setting, onComplated);
            var dialog = new PrintDialog();
            dialog.Document = document;

            if (setting != null) config(dialog);
            //dialog.AllowPrintToFile = true;
            //dialog.AllowCurrentPage = true;
            //dialog.AllowSomePages = true;
            //dialog.UseEXDialog = true;

            // 用户确认
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;

            document.Print();
            return true;
        }
    }
}
