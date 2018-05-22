using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;

namespace Epic.FluentAPI
{
    /// <summary>
    /// System.Data.Entity.ModelConfiguration.Utilities
    /// 引用自 PropertyPath, 略做修改
    /// </summary>
    public class PropertyPath : IEnumerable<PropertyInfo>, IEnumerable
    {
        public static readonly PropertyPath Empty = new PropertyPath();

        readonly List<PropertyInfo> components = new List<PropertyInfo>();


        PropertyPath()
        {
        }

        public PropertyPath(PropertyInfo component)
        {
            this.components.Add(component);
        }

        public PropertyPath(IEnumerable<PropertyInfo> components)
        {
            this.components.AddRange(components);
        }

        public PropertyInfo this[int index]
        {
            get { return this.components[index]; }
        }

        public int Count
        {
            get { return this.components.Count; }
        }

        public override string ToString()
        {
            if (this.components.Count == 0) return String.Empty;
            var result = new List<string>();
            this.components.ForEach(e => result.Add(e.Name));
            return String.Join(".", result);
        }

        public bool Equals(PropertyPath value)
        {
            if (Object.ReferenceEquals(null, value)) return false;

            if (Object.ReferenceEquals(this, value)) return true;

            return this.components.SequenceEqual(value.components, (PropertyInfo p1, PropertyInfo p2) => p1.IsSame(p2));
        }

        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(PropertyPath)) && this.Equals((PropertyPath)obj)));
        }

        public override int GetHashCode()
        {
            return this.components.Aggregate(0, (int t, PropertyInfo n) => t + n.GetHashCode());
        }

        public static bool operator ==(PropertyPath left, PropertyPath right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(PropertyPath left, PropertyPath right)
        {
            return !object.Equals(left, right);
        }

        IEnumerator<PropertyInfo> IEnumerable<PropertyInfo>.GetEnumerator()
        {
            return this.components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.components.GetEnumerator();
        }

    }
}
