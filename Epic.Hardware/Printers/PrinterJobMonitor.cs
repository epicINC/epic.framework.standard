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

        public PrinterJobMonitor(PrintQueue queue, int timeout, int interval, CancellationTokenSource source = null) : this(queue, TimeSpan.FromMilliseconds(timeout), interval, source)
        {
        }

        public PrinterJobMonitor(PrintQueue queue, TimeSpan timeout, int interval, CancellationTokenSource source = null)
        {
            this.Queue = queue;
            this.Timeout = timeout;
            this.Interval = interval;
            this.Source = source ?? new CancellationTokenSource();
        }

        public PrintQueue Queue { get; private set; }

        public CancellationTokenSource Source { get; private set; }
        public bool IsWorking { get; private set; }

        public TimeSpan Timeout { get; private set; }
        public int Interval { get; private set; }

        public event Action<int, int> Changed;


        public async Task Start()
        {
            await this.Start(this.Source.Token);
        }

        internal async Task Start(CancellationToken token)
        {
            var exists = new HashSet<int>();
            int previousTotal = 0, total = 0, previousPrinted = 0, printed = 0, jobsCount;
            PrintJobInfoCollection jobs;

            this.Source.CancelAfter(this.Timeout);
            while (!token.IsCancellationRequested)
            {
                this.Queue.Refresh();
                jobs = this.Queue.GetPrintJobInfoCollection();
                jobsCount = 0;
                foreach (var item in jobs)
                {
                    jobsCount++;
                    if (exists.Contains(item.JobIdentifier)) continue;
                    exists.Add(item.JobIdentifier);
                }
                if (token.IsCancellationRequested) return;
                printed = (total = exists.Count) - jobsCount;
                if (previousPrinted != printed || previousTotal != total)
                {
                    this.Source.CancelAfter(this.Timeout);
                    this.OnChanged(previousPrinted = printed, previousTotal = total);
                }

                await Task.Delay(this.Interval);
            }
        }

        public void Stop()
        {
            this.Source.Cancel();
        }


        void OnChanged(int printed, int total)
        {
            if (this.Changed == null) return;
            this.Changed(printed, total);
        }

        public void Dispose()
        {
            this.Source.Dispose();
            this.Queue.Dispose();
        }
    }
}
