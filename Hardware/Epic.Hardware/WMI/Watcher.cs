using System;
using System.Collections.Generic;
using System.Management;
using System.Security;
using System.Text;

namespace Epic.Hardware.WMI
{
    public class Watcher : IDisposable
    {
        protected Watcher()
        {
        }

        public Watcher(string query, bool autoStart = false)
        {
            this.Query = query;
            if (this.AutoStart = autoStart)
                this.Start();
        }

        public string Query { get; protected set; }
        public bool AutoStart { get; protected set; }

        event Action<Watcher, ManagementBaseObject> Changed;


        ManagementEventWatcher EventWatcher { get; set; }


  
        public void Start()
        {
            if (this.EventWatcher == null)
            {
                //var query = new WqlEventQuery(sql);
                this.EventWatcher = new ManagementEventWatcher(this.Query);
                this.EventWatcher.EventArrived += Arrived;
            }
            this.EventWatcher.Start();

        }

        public void Stop()
        {
            if (this.EventWatcher == null) return;
            this.EventWatcher.Stop();
        }

        void Arrived(object sender, EventArrivedEventArgs e)
        {
            var instance = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;
            if (instance == null) return;
            this.OnChanged(instance);
        }

        protected virtual void OnChanged(ManagementBaseObject e)
        {
            if (this.Changed == null) return;
            this.Changed(this, e);
        }

        public static ManagementBaseObject WaitForEvent(string query, int timeout, Func<ManagementBaseObject, bool> predicate)
        {
            ManagementBaseObject result;
            try
            {
                using (var watcher = new ManagementEventWatcher(query))
                {
                    watcher.Options.Timeout = TimeSpan.FromSeconds(timeout);
                    while (!predicate(result = watcher.WaitForNextEvent()))
                    {
                    }
                    return result;
                }
            }
            catch(ManagementException e)
            {

            }

            return null;
        }



        public void Dispose()
        {
            this.Stop();
        }



      

    }
}
