using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Components
{
    /// <summary>
    /// 密码加密类型
    /// </summary>
    public enum PasswordEncryptType
    {
        /// <summary>
        /// 明文
        /// </summary>
        PlanText = 1 << 0,

        Salt = 1 << 1,

        /// <summary>
        /// MD5 
        /// </summary>
        MD516 = 1 << 2,

        /// <summary>
        /// MD5
        /// </summary>
        MD532 = 1 << 3,


        SHA1 = 1 << 4,
        SHA256 = 1 << 5,
        SHA512 = 1 << 6,

        MD516Salt = MD516 | Salt,
        MD532Salt = MD532 | Salt,

        SHA1Salt = SHA1 | Salt,
        SHA256Salt = SHA256 | Salt,
        SHA512Salt = SHA512 | Salt,
    }
}