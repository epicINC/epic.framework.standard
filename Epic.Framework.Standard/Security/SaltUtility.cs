using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Security
{
    public static partial class Utility
    {

        public static string RND()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }




        #region Salt

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>byte[] 数组</returns>
        public static byte[] SaltCustom(int length)
        {
            byte[] data = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>生成结果</returns>
        public static string Salt(int length = 16)
        {
            return Epic.Converters.HexString.Encode(SaltCustom(length / 2));
        }

        #endregion

    }
}
