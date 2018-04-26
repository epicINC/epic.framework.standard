using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Caches
{

    public static class EpicRuntimeCache
    {

        static EpicCacheDictionary current;

        public static EpicCacheDictionary Current
        {
            get
            {
                if (current == null)
                    current = new EpicCacheDictionary();
                return current; 
            }
        }

        public static void Set(string key, object value)
        {
            Current[key] = value;
        }

        public static T Get<T>(string key)
        {
            return (T)Current[key];
        }

        public static dynamic Bag
        {
            get { return Current.Bag; }
        }


        public static bool IsReadonly<T>()
        {
            return EpicRuntimeCache<T>.IsReadonly;
        }

        public static Func<T> Init<T>()
        {
            return EpicRuntimeCache<T>.Init;
        }

        public static void Init<T>(Func<T> value)
        {
            EpicRuntimeCache<T>.Init = value;
        }

        public static DateTime DateStamp<T>()
        {
            return EpicRuntimeCache<T>.Expiry;
        }

        public static int Interval<T>()
        {
            return EpicRuntimeCache<T>.Interval;
        }

        public static void Interval<T>(int value)
        {
            EpicRuntimeCache<T>.Interval = value;
        }

        public static int Counter<T>()
        {
            return EpicRuntimeCache<T>.Counter;
        }



        public static bool IsExpired<T>()
        {
            return EpicRuntimeCache<T>.IsExpired();
        }


        public static T Value<T>()
        {
            return EpicRuntimeCache<T>.Value;
        }

        public static void Value<T>(T value)
        {
            EpicRuntimeCache<T>.Value = value;
        }



        public static void Clear<T>()
        {
            EpicRuntimeCache<T>.Clear();
        }

        public static void ClearInterval<T>()
        {
            EpicRuntimeCache<T>.ClearInterval();
        }

        public static void ClearValue<T>()
        {
            EpicRuntimeCache<T>.ClearValue();
        }

    }

    public static class EpicRuntimeCache<T>
    {
        static EpicRuntimeCache()
        {
            Cache = new EpicCacheItem<T>();
        }

        public static void Readonly(T value, int interval = 0)
        {
            Cache = EpicCacheItem.Create(value, interval);
        }

        public static void Readonly(Func<T> value, int interval = 0)
        {
            Cache = EpicCacheItem.Create(value, interval);
        }




        static IEpicCacheItem<T> Cache
        {
            get;
            set;
        }


        public static bool IsReadonly
        {
            get { return Cache.IsReadonly; }
        }

        public static Func<T> Init
        {
            get { return Cache.Init; }
            set { Cache.Init = value; }
        }

        public static DateTime Expiry
        {
            get { return Cache.Expiry; }
        }

        public static int Interval
        {
            get { return Cache.Interval; }
            set { Cache.Interval = value; }
        }

        public static int Counter
        {
            get { return Cache.Counter; }
        }

        public static bool IsExpired()
        {
            return Cache.IsExpired();
        }

        public static T Value
        {
            get { return Cache.Value; }
            set { Cache.Value = value; }
        }

        public static void Clear()
        {
            Cache.Clear();
        }

        public static void ClearInterval()
        {
            Cache.ClearInterval();
        }

        public static void ClearValue()
        {
            Cache.ClearValue();
        }
    }
}
