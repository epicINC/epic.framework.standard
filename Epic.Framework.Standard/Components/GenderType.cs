using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Epic.Components
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType : byte
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("保密")]
        Undefined,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female,
    }
}
