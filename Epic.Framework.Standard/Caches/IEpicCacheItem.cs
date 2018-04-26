using System;
namespace Epic.Caches
{
    public interface IEpicCacheItem<T>
    {
        bool IsReadonly { get; }


        Func<T> Init { get; set; }

        DateTime Expiry { get; }
        int Interval { get; set; }

        int Counter { get; }

        bool IsExpired();

 

        T Value { get; set; }

        void Clear();
        void ClearInterval();
        void ClearValue();
    }
}
