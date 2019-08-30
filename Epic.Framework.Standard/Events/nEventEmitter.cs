using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Events
{

    internal class EventEmitterData
    {
        public string Name { get; set; }


        public Delegate Listener { get; set; }


        public Dictionary<Delegate, EventEmitterItem> Data { get; set; } = new Dictionary<Delegate, EventEmitterItem>();
    }

    internal class EventEmitterItem
    {
        public bool Fired { get; set; }

        public bool Once { get; set; }

        public int Count { get; set; }

        public Delegate Listener { get; set; }

        public Delegate Wrapper { get; set; }
    }


    public class EventEmitter
    {
        Dictionary<string, EventEmitterData> Map = new Dictionary<string, EventEmitterData>();


        public IEnumerable<string> EventNames
        {
            get { return this.Map.Keys; }
        }

        public int ListenerCount(string eventName)
        {
            if (!this.Map.TryGetValue(eventName, out var result)) return 0;
            return result.Listener.GetInvocationList().Length;
        }


        public IEnumerable<Delegate> Listeners(string eventName)
        {
            if (!this.Map.TryGetValue(eventName, out var result)) return default;
            return result.Listener.GetInvocationList();
        }

        public Action<T>[] Listeners<T>(string eventName)
        {
            if (!this.Map.TryGetValue(eventName, out var result)) return default;
            return result.Listener.GetInvocationList() as Action<T>[];
        }


        bool Exists(string name, Delegate listener)
        {
            return this.Map.TryGetValue(name, out var item) && item.Data.ContainsKey(listener);
        }

        bool Set<T>(string name, Action<T> listener, Func<EventEmitterItem, Delegate> wrapper, bool prepend = false)
        {
            if (this.Map.TryGetValue(name, out var item) && item.Data.ContainsKey(listener)) return false;

            if (item == null)
                this.Map.Add(name, item = new EventEmitterData());


            var e = new EventEmitterItem()
            {
                Fired = false,
                Listener = listener
            };

            if (wrapper != null)
                e.Wrapper = wrapper(e);

            item.Data.Add(listener, e);

            if (item.Listener != null)
                item.Listener = prepend ? MulticastDelegate.Combine(e.Wrapper ?? e.Listener, item.Listener) : MulticastDelegate.Combine(item.Listener, e.Wrapper ?? e.Listener) ;
            else
                item.Listener = e.Wrapper ?? e.Listener;

            this.Emit("newListener", name, listener);
            return true;
        }

        Delegate Get(string name)
        {
            this.Map.TryGetValue(name, out var result);
            return result.Listener;
        }

        #region Prepend Once and On


        void Add(string eventName, Delegate listener)
        {
            //this.Set()
        }

        public EventEmitter PrependOnceListener<T>(string eventName, Action<T> listener)
        {
            this.Set<T>(eventName, listener, null, true); ;
            return this;
        }

        public EventEmitter Once<T>(string eventName, Action<T> listener)
        {
            //this.Set<T>(eventName, listener, this.OnceWrapper);
            return this;
        }

        public EventEmitter PrependListener<T>(string eventName, Action<T> listener)
        {
            this.Set<T>(eventName, listener, null, true);
            return this;
        }

        public EventEmitter On<T>(string eventName, Action<T> listener)
        {
            this.Set<T>(eventName, listener, null);
            return this;
        }

        #endregion

        #region Emit

        public EventEmitter Emit<T>(string eventName)
        {
            var listener = this.Get(eventName) as Action;
            if (listener != null) listener();
            return this;
        }

        public EventEmitter Emit<T>(string eventName, T value)
        {
            var listener = this.Get(eventName) as Action<T>;
            if (listener != null) listener(value);
            return this;
        }

        public EventEmitter Emit<T, K>(string eventName, T args1, K args2)
        {
            var listener = this.Get(eventName) as Action<T, K>;
            if (listener != null) listener(args1, args2);
            ;
            return this;
        }

        public EventEmitter Emit<T, K, I>(string eventName, T args1, K args2, I args3)
        {
            var listener = this.Get(eventName) as Action<T, K, I>;
            if (listener != null) listener(args1, args2, args3);
            return this;
        }

        #endregion

        #region Off



        public EventEmitter removeAllListeners(string eventName = null)
        {
            if (eventName != null)
                this.Map.Remove(eventName);
            else
                this.Map.Clear();
            return this;
        }

        public bool Off(string eventName, Delegate listener)
        {
            //if (!this.Indexs.TryGetValue(eventName, out var indexs)) return false;
            //if (!indexs.TryGetValue(listener, out var pack)) return false;
            //if (!this.Map.TryGetValue(eventName, out var result)) return false;

            //if (result == pack)
            //{
            //    this.Map.Remove(eventName);
            //    this.Indexs.Remove(eventName);
            //}
            //else
            //{
            //    this.Map[eventName] = MulticastDelegate.Remove(result, pack);
            //    indexs.Remove(listener);
            //}
            //this.Emit("removeListener", eventName, listener);
            return true;
        }

        #endregion

    }
}
