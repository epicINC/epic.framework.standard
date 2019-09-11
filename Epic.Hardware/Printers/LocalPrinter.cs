using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Management;
using Epic.Hardware.WMI;
using System.Printing;
using System.Threading.Tasks;
using System.Threading;

namespace Epic.Hardware.Printers
{
    public class LocalPrinter
    {
        public LocalPrinter(string name)
        {
            this.Name = name;

        }

        public string Name { get; set; }




        public static void DefaultJobStauts(int timeout, int interval, Action<int, int> action)
        {
            JobStatus3(DefaultQueue, timeout, interval, action);
        }

        public static async Task JobStatus(PrintQueue queue, int timeout, int interval,  Action<int> action)
        {
            using (queue)
            {
                int previousTotal = 0, total = 0, printed = 0, i = 0, count = timeout * 1000 / interval;

                while (true)
                {
                    queue.Refresh();
                    total = queue.GetPrintJobInfoCollection().Count();
                    if (i++ > count) return;
                    if (previousTotal != total)
                    {
                        i = 0;
                        if (total < previousTotal)
                        {
                            printed += previousTotal - total;
                            action(printed);
                        }
                        previousTotal = total;
                    }
                    await Task.Delay(interval);
                }
            }
        }


        public static async Task JobStatus2(PrintQueue queue, int timeout, int interval, Action<int, int> action, CancellationToken token = default)
        {
            using (queue)
            {
                int previousTotal = 0, total = 0, previousPrinted = 0, printed = 0, i = 0, count = timeout * 1000 / interval;
                PrintJobInfoCollection jobs;

                var exists = new HashSet<int>();
                while (!token.IsCancellationRequested)
                {
                    queue.Refresh();
                    jobs = queue.GetPrintJobInfoCollection();
                    foreach (var item in jobs)
                    {
                        if (exists.Contains(item.JobIdentifier)) continue;
                        exists.Add(item.JobIdentifier);
                    }
                    if (i++ > count) return;
                    printed = (total = exists.Count) - jobs.Count();
                    if (previousPrinted != printed || previousTotal != total)
                        action(previousPrinted = printed, previousTotal = total);

                    await Task.Delay(interval);
                }
            }
        }


        public static void JobStatus3(PrintQueue queue, int timeout, int interval, Action<int, int> action, CancellationTokenSource source = null)
        {
            var monitor = new PrinterJobMonitor(queue, timeout, interval, source);
            monitor.Changed += action;
            _ = monitor.Start();
        }

        public static void JobStatus3(PrintQueue queue, int timeout, int interval, Action<int, int, PrinterJobMonitor> action, CancellationTokenSource source = null)
        {
            var monitor = new PrinterJobMonitor(queue, timeout, interval, source);
            monitor.Changed += (printed, total) => action(printed, total, monitor);
            _ = monitor.Start();
        }

        public static PrintQueue DefaultQueue
        {
            get
            {
                return LocalPrintServer.GetDefaultPrintQueue();
            }
        }


    }
}
