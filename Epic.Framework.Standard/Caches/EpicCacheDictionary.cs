using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Dynamic;
using Epic.Collections;

namespace Epic.Caches
{
    public class EpicCacheDictionary : MarshalByRefObject
    {


        public void Add(string key, object value)
        {
            this.Data[key] = new EpicCacheItem<object>(value);
        }

        public void Add(string key, object value, DateTime expiry)
        {
            this.Data[key] = new EpicCacheItem<object>(value, expiry);
        }




        public object this[string key]
        {
            get { return this.Data[key]; }
            set { this.Data[key].Value = value; }
        }


        DynamicFuncDataDictionary<DataDictionary<EpicCacheItem<object>>> bag;

        public dynamic Bag
        {
            get
            {
                if (this.bag == null)
                    //this.bag = new DynamicDataDictionary<DataDictionary<EpicCacheItem<object>>>(() => this.Data);
                {
                    this.bag = new DynamicFuncDataDictionary<DataDictionary<EpicCacheItem<object>>>(
                            () => this.Data,
                            (GetMemberBinder e, out object k) => { k = this.Data[e.Name].Value; return true; },
                            (e, k) => { this.Data[e.Name].Value = k;return true; }
                        );
                }
                return this.bag;
            }
        }



        DataDictionary<EpicCacheItem<object>> data;

        public DataDictionary<EpicCacheItem<object>> Data
        {
            get
            {
                if (this.data == null)
                    this.data = new DataDictionary<EpicCacheItem<object>>();
                return this.data;
            }
        }


        public int Count
        {
            get { return this.Data.Count; }
        }


        

    }
}
