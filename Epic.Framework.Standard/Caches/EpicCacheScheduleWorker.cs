using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Epic.Caches
{
    public class EpicCacheScheduleWorker
    {

        public EpicCacheConfig Config
        {
            get;
            set;
        }


        public EpicCacheDictionary Data
        {
            get;
            set;
        }


        Timer timer;

        public void Start()
        {
            if (this.timer == null)
                this.timer = new Timer();
            this.timer.Elapsed += this.OnTimerElapsed;

            
        }

        protected void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {

        }


        public void Task()
        {
            if (this.Data == null || this.Data.Count == 0) return;
            for (int i = 0; i < this.Data.Count; i++)
            {
                
            }

        }


    }
}
