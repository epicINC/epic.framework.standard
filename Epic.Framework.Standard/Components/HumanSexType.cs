using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Epic.Components
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum HumanSexType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("保密")]
        Undefine = 0,

        /// <summary>
        /// 无性人
        /// </summary>
        [Description("中性")]
        Asexual = 1,

        [Description("女性")]
        Female = 1 << 1,

        [Description("男性")]
        Male = 1 << 2,


        /// <summary>
        /// 双性人
        /// </summary>
        [Description("双性")]
        Both = Female | Male,

    }
}

