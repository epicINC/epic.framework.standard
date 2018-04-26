using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Caches
{
    public static class EpicCacheItem
    {
        public static EpicCacheItem<T> Create<T>()
        {
            return new EpicCacheItem<T>();
        }

        public static EpicCacheItem<T> Create<T>(T value, int interval = 0)
        {
            return new EpicCacheItem<T>(value, 0, false);
        }

        public static EpicCacheItem<T> Create<T>(Func<T> value, int interval = 0)
        {
            return new EpicCacheItem<T>(value, 0, false);
        }

        public static EpicCacheItem<T> Readyonly<T>(T value, int interval = 0)
        {
            return new EpicCacheItem<T>(value, 0, true);
        }

        public static EpicCacheItem<T> Readyonly<T>(Func<T> value, int interval = 0)
        {
            return new EpicCacheItem<T>(value, 0, true);
        }

    }

    public class EpicCacheItem<T> : IEpicCacheItem<T>
    {
        public EpicCacheItem()
        {
            this.Calc = Empty;
        }

        internal EpicCacheItem(T value) : this(value, 0, false)
        {
        }

        internal EpicCacheItem(Func<T> value) : this(value, 0, false)
        {
        }

        internal EpicCacheItem(T value, DateTime expiry) : this(value, expiry, false)
        {
        }

        internal EpicCacheItem(Func<T> value, DateTime expiry) : this(value, expiry, false)
        {
        }


        internal EpicCacheItem(T value, DateTime expiry, bool isReadonly)
        {

            this.InnerValue = value;
            this.Expiry = expiry;
            this.IsReadonly = isReadonly;
            this.Bind();
        }

        internal EpicCacheItem(Func<T> value, DateTime expiry, bool isReadonly)
        {
            this.InnerInit = value;
            this.Expiry = expiry;
            this.IsReadonly = isReadonly;
            this.Bind();
        }

        internal EpicCacheItem(T value, int interval, bool isReadonly)
        {
            this.InnerValue = value;
            this.InnerInterval = interval;
            this.IsReadonly = isReadonly;
            this.Bind();
        }



        internal EpicCacheItem(Func<T> value, int interval, bool isReadonly)
        {
            this.InnerInit = value;
            this.InnerInterval = interval;
            this.IsReadonly = isReadonly;
            this.Bind();
        }


        static T Empty()
        {
            return default(T);
        }



        Func<T> InnerInit
        {
            get;
            set;
        }


        public Func<T> Init
        {
            get { return this.InnerInit; }
            set
            {
                this.InnerInit = value;
                this.Bind();
            }
        }


        Func<T> Calc
        {
            get;
            set;
        }


        protected virtual void Bind()
        {
            if (this.Interval > 0 || this.Expiry != DateTime.MinValue)
            {
                if (this.InnerInit == null)
                    this.Calc = this.LoadValueWithExpired;
                else
                    this.Calc = this.LazyLoadValueWithExpired;
                return;
            }


            if (this.InnerInit == null)
                this.Calc = this.LoadValue;
            else
                this.Calc = this.LazyLoadValue;
 
        }

        protected virtual T LoadValue()
        {
            return this.InnerValue;
        }

        protected virtual T LazyLoadValue()
        {
            if (this.InnerValue == null)
                this.InnerValue = this.Init();
            return this.InnerValue;
        }

        protected virtual T LoadValueWithExpired()
        {
            if (this.InnerValue != null && this.IsExpired())
                this.InnerValue = default(T);
            return this.InnerValue;
        }

        protected virtual T LazyLoadValueWithExpired()
        {
            if (this.InnerValue == null || this.IsExpired())
                this.InnerValue = this.Init();
            return this.InnerValue;
        }


        public bool IsExpired()
        {
            return this.IsExpired(DateTime.Now);
        }

        bool IsExpired(DateTime value)
        {
            if (value > this.Expiry)
            {
                if (this.Interval > 0)
                    this.Expiry = value.AddSeconds(this.Interval);
                return true;
            }
            return false;
        }




        protected T InnerValue
        {
            get;
            set;
        }

        protected int InnerInterval
        {
            get;
            set;
        }

        public bool IsReadonly
        {
            get;
            private set;
        }

        public DateTime Expiry
        {
            get;
            private set;
        }

        public int Counter
        {
            get;
            private set;
        }

        public int Interval
        {
            get { return this.InnerInterval; }
            set
            {
                this.InnerInterval = value;
                this.Bind();
            }
        }

        public T Value
        {
            get { return this.Calc(); }
            set
            {
                if (this.IsReadonly)
                    throw new ApplicationException(String.Format("Cache({0} readonly)", typeof(T)));
                this.InnerValue = value;
                this.Bind();
            }
        }

        public void ClearValue()
        {
            this.InnerValue = default(T);
        }

        public void ClearInterval()
        {
            this.Interval = 0;
            this.Expiry = DateTime.MinValue;
        }

        public void Clear()
        {
            this.Calc = Empty;
            this.InnerInit = null;
            this.ClearInterval();
            this.ClearValue();
        }
        
    }
}
