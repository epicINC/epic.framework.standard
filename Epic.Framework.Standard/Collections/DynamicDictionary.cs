using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Collections
{
    public class DynamicDictionary : Dictionary<string, object>
    {







        public Dictionary<string, object> Data
        {
            get { return this; }
        }

        DynamicFuncDataDictionary<Dictionary<string, object>> bag;

        public dynamic Bag
        {
            get
            {
                if (this.bag != null) return this.bag;
                return this.bag = new DynamicFuncDataDictionary<Dictionary<string, object>>(
                            () => this.Data,
                            (GetMemberBinder e, out object k) => { k = this.Data[e.Name]; return true; },
                            (e, k) => { this.Data[e.Name] = k; return true; }
                        );
            }
        }

    }
}
