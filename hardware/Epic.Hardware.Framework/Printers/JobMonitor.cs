using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epic.Hardware.Printers
{
    public class PrinterJobMonitor : IDisposable
    {


        public static PrinterJobMonitor Default
        {
            get
            {
                return new PrinterJobMonitor(PrinterJob.DefaultQueue, TimeSpan.FromSeconds(20), 100);
            }
        }


        public PrinterJobMonitor(PrintQueue queue, int timeout, int interval)
        {
            Init(queue, timeout, interval);
        }

        public PrinterJobMonitor(PrintQueue queue, TimeSpan timeout, int interval)
        {
            var value = (long)timeout.TotalMilliseconds;
            if (value < -1 || value > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("timeout");

            this.Init(queue, (int)value, interval);

        }

        void Init(PrintQueue queue, int timeout, int interval)
        {
            this.Queue = queue;
            this.Timeout = timeout;
            this.Interval = interval;
        }

        public PrintQueue Queue { get; private set; }

        public bool IsWorking { get; private set; }

        public int Timeout { get; private set; }
        public int Interval { get; private set; }

        bool IsCancel;

        HashSet<int> JobIDS = new HashSet<int>();


        public async Task Start()
        {
            int previousTotal = 0, total = 0, previousPrinted = 0, printed = 0, jobsCount;
            int i = 0, count = this.Timeout / this.Interval;
            PrintJobInfoCollection jobs;


            while (!this.IsCancel)
            {
                this.Queue.Refresh();
                jobs = this.Queue.GetPrintJobInfoCollection();
                jobsCount = 0;
                foreach (var item in jobs)
                {
                    jobsCount++;
                    if (this.JobIDS.Contains(item.JobIdentifier)) continue;
                    this.JobIDS.Add(item.JobIdentifier);
                }
                if (this.IsCancel) return;
                if (++i > count)
                {
                    this.OnTimeouted(printed, total);
                    return;
                }
                printed = (total = this.JobIDS.Count) - jobsCount;
                if (previousPrinted != printed || previousTotal != total)
                {
                    i = 0;
                    this.OnChanged(previousPrinted = printed, previousTotal = total);
                }

                await Task.Delay(this.Interval);
            }
        }

        public void Stop()
        {
            this.IsCancel = true;
        }

        public void Reset()
        {
            this.JobIDS.Clear();
        }

        public event Action<int, int> Changed;

        void OnChanged(int printed, int total)
        {
            if (this.Changed == null) return;
            this.Changed(printed, total);
        }

        public event Action<int, int> Timeouted;

        void OnTimeouted(int printed, int total)
        {
            if (this.Timeouted == null) return;
            this.Timeouted(printed, total);
        }


        public void Dispose()
        {
            this.Queue.Dispose();
        }
    }
}
