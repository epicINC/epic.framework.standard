
using Epic.Hardware.WMI;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace WpfApp1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Epic.Hardware.Printers.LocalPrinter.DefaultJobStauts(20000000, 50, (printed, total) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.tip.Text += $"printed: {printed}, total: {total}\n";
                });
            });

            return;
            var watcher = PrinterWatcher.Default;
            watcher.Printed += Watcher_Printed;
            watcher.Idle += Watcher_Idle;
            watcher.Start();

        }



        private void Watcher_Idle(PrinterWatcher arg1, Win32Printer arg2)
        {
        }

        private static void Watcher_Printed(PrinterWatcher arg1, Win32Printer arg2)
        {
            Console.WriteLine("Printed");
        }
    }
}
