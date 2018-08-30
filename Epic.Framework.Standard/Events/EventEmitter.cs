using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Epic.Events
{

    public class EventEmitter
    {

        Dictionary<string, List<Listener>> Map = new Dictionary<string, List<Listener>>();

        
        public void Once(string name, Delegate action)
        {
            var item = new Listener(action);

            item.Invoked += () => this.Off(name, action);

        }

        void Add(string name, Listener value)
        {
            if (!this.Map.TryGetValue(name, out List<Listener> result))
                this.Map.Add(name, result = new List<Listener>());
            result.Add(value);
            this.Emit("newListener", name, value);
        }

        public EventEmitter On(string name, Delegate action)
        {
            this.Add(name, new Listener(action));
            return this;
        }

        public void On(string name, Action action)
        {
            this.On(name, (Delegate)action);
        }

        public void On<T>(string name, Action<T> action)
        {
            this.On(name, (Delegate)action);
        }

        public void On<T, K>(string name, Action<T, K> action)
        {
            this.On(name, (Delegate)action);
        }

        public void Emit(string name, params object[] args)
        {
            if (!this.Map.TryGetValue(name, out List<Listener> result)) return;
            result.ForEach(e => e.Invoke(args));
        }

        public EventEmitter Off(string name, Delegate action)
        {
            if (!this.Map.TryGetValue(name, out List<Listener> result)) return this;

            var data = result.Where(e => e.Action == action).ToArray();

            data.ForEach(e =>
            {
                result.Remove(e);
                this.Emit("removeListener", name, e);
            });

            return this;
        }


        public EventEmitter RemoveAll(string name = null)
        {
            if (name == null)
                this.Map.Clear();
            else
                this.Map.Remove(name);
            return this;
        }


        public int Count(string name = null)
        {
            if (name == null) return this.Map.Values.Sum(e => e.Count);
            if (this.Map.TryGetValue(name, out List<Listener> result)) return result.Count;
            return 0;

        }


        public IEnumerable<Delegate> Listeners(string name = null)
        {
            if (name == null) return this.Map.Values.SelectMany(e => e.Select(x => x.Action));
            if (this.Map.TryGetValue(name, out List<Listener> result)) return result.Select(e => e.Action);
            return new Delegate[0];
        }


        public string[] Names { get => this.Map.Keys.ToArray(); }
    }
}
