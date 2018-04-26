using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Collections
{

    public sealed class DynamicDataDictionary<T> : DynamicObject where T : IDictionary<string, object>
    {
        readonly Func<T> thunk;

        T Data
        {
            get { return this.thunk(); }
        }

        public DynamicDataDictionary(Func<T> thunk)
        {
            this.thunk = thunk;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {

            return this.Data.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Data[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.Data[binder.Name] = value;
            return true;
        }
    }
}
