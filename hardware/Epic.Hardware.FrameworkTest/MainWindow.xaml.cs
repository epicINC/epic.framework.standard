using Epic.Hardware.Framework;
using Epic.Hardware.Framework.Printers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace Epic.Hardware.FrameworkTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        const string XpsFile = @"./data/1592549279330.xps";
        const string PdfFile = @"./data/1592549279330.pdf";
        const string Printer = "DL-581P";


        Action<Action<XpsDocument>> OpenXps(string path)
        {
            return (Action<XpsDocument> e) =>
            {
                using (var doc = new XpsDocument(path, System.IO.FileAccess.Read))
                {
                    e(doc);
                }
            };
        }

        Action<Action<Stream>> OpenStream(string path)
        {
            return (Action<Stream> e) =>
            {
                using (var stream = File.OpenRead(path))
                {
                    e(stream);
                }
            };
        }

        #region WPF Printer

        void WPFPrintXpsDocumentClick(object sender, RoutedEventArgs e)
        {
            OpenXps(XpsFile)(val => WPFPrinter.Print(val, Printer));
        }

        void WPFPrintXpsStreamClick(object sender, RoutedEventArgs e)
        {
            OpenStream(XpsFile)(val => WPFPrinter.Print(val, Printer));
        }

        void WPFPrintXpsFileClick(object sender, RoutedEventArgs e)
        {
           WPFPrinter.Print(XpsFile, Printer);
        }

        void WPFPrintXpsDialogClick(object sender, RoutedEventArgs e)
        {
            OpenXps(XpsFile)(val => WPFPrinter.Dialog(val, printer: Printer));
        }

        void ProcessLPRClick(object sender, RoutedEventArgs e)
        {
            ProcessPrinter.LPR(PdfFile, printer: Printer);
        }


        #endregion
    }
}
