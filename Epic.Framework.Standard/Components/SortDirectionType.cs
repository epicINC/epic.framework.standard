using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 排序方向
    /// </summary>
    public enum SortDirectionType : byte
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,

        /// <summary>
        /// 倒序
        /// </summary>
        Desc = 1,

        /// <summary>
        /// 正序
        /// </summary>
        Asc = 2
    }
}
