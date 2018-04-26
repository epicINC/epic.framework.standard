using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Caches
{
    public class EpicCacheConfig
    {

        static EpicCacheConfig instance;

        public static EpicCacheConfig Current
        {
            get
            {
                if (instance == null)
                    instance = new EpicCacheConfig();
                return instance;
            }
        }


        public int LowCounter
        {
            get;
            set;
        }
    }
}
