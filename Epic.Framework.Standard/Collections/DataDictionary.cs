using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Collections
{
    public class DataDictionary<T> : IDictionary<string, T>
    {
        public DataDictionary()
        {
            this.Dictionary = new Dictionary<string, T>();
        }

        readonly Dictionary<string, T> Dictionary;


        public void Add(string key, T value)
        {
            this.Dictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return this.Dictionary.Keys; }
        }

        public bool Remove(string key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out T value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        public ICollection<T> Values
        {
            get { return this.Dictionary.Values; }
        }

        public T this[string key]
        {
            get
            {
                T result;
                this.Dictionary.TryGetValue(key, out result);
                return result;
            }
            set { this.Dictionary[key] = value; }
        }

        public void Add(KeyValuePair<string, T> item)
        {
            ((ICollection<KeyValuePair<string, T>>)this.Dictionary).Add(item);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return ((ICollection<KeyValuePair<string, T>>)this.Dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, T>>)this.Dictionary).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.Dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<string, T>>)this.Dictionary).IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            return ((ICollection<KeyValuePair<string, T>>)this.Dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Dictionary).GetEnumerator();
        }
    }
}
