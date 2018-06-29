using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Epic
{
    public static class Arguments
    {
        /// <summary>
        /// 全部有 true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(params string[] value)
        {
            return value.Any(String.IsNullOrWhiteSpace);
        }

        public static bool IsNullOrEmpty(params object[] value)
        {
            return value.Any(IsNullOrEmpty);
        }

        /// <summary>
        /// 有值 true, 无值或空 false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object value)
        {
            switch (value)
            {
                case null:
                    return true;
                case string s when s.Length == 0 || s.Any(Char.IsWhiteSpace):
                case Stream stream when stream.Length == 0:
                    return true;
                default:
                    return false;
            }
        }
    }
}
