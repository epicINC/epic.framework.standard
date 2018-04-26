using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 范围
    /// </summary>
    public class Range<T>
    {

        public Range()
        {
        }

        public Range(T min, T max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public T Min
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public T Max
        {
            get;
            set;
        }
    }
}
