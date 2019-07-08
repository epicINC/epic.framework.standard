using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Epic.Components;
using Epic.Extensions;

namespace Epic.Security
{
    /// <summary>
    /// 加密类
    /// </summary>
    public static class HashAlgorithm
    {
        public static string Hash(PasswordEncryptType encryptType, string format, string password, string salt)
        {
            switch (encryptType)
            {
                case PasswordEncryptType.PlanText:
                    return HashFormat(format, password, salt);

                case PasswordEncryptType.MD516:
                    return MD5(HashFormat(format, password)).Substring(8, 16);
                case PasswordEncryptType.MD532:
                    return MD5(HashFormat(format, password));

                case PasswordEncryptType.MD516Salt:
                    return MD5(HashFormat(format, password, salt)).Substring(8, 16);
                case PasswordEncryptType.MD532Salt:
                    return MD5(HashFormat(format, password, salt));

                case PasswordEncryptType.SHA1:
                    return SHA1(HashFormat(format, password));
                case PasswordEncryptType.SHA256:
                    return SHA256(HashFormat(format, password));
                case PasswordEncryptType.SHA512:
                    return SHA512(HashFormat(format, password));

                case PasswordEncryptType.SHA1Salt:
                    return SHA1(HashFormat(format, password, salt));
                case PasswordEncryptType.SHA256Salt:
                    return SHA256(HashFormat(format, password, salt));
                case PasswordEncryptType.SHA512Salt:
                    return SHA512(HashFormat(format, password, salt));
                case PasswordEncryptType.Salt:
                    return salt ?? Utility.Salt();
                default:
                    break;
            }
            return password;
        }

        static string HashFormat(string format, string password)
        {
            if (!String.IsNullOrEmpty(format))
                return String.Format(format, password);
            return password;
        }

        static string HashFormat(string format, string password, string salt)
        {
            if (!String.IsNullOrEmpty(format))
                return String.Format(format, password, salt);
            return password + salt;
        }

   



        static string Hash(string algorithm, string value)
        {
            return System.Security.Cryptography.HashAlgorithm.Create(algorithm).ComputeHash(Encoding.UTF8.GetBytes(value)).ToHex();
        }


        /// <summary>
        /// MD5 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string MD5(this string value)
        {
            return Hash("MD5", value);
        }

        /// <summary>
        /// SHA1 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string SHA1(this string value)
        {
            return Hash("SHA1", value);
        }

        public static string SHA256(this string value)
        {
            return Hash("SHA256", value);
        }

        public static string SHA512(this string value)
        {
            return Hash("SHA512", value);
        }


        /// <summary>
        /// 动网 MD5 算法
        /// </summary>
        /// <param name="encode">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string DVMD5(string encode)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(encode);
            byte[] hashValue = ((System.Security.Cryptography.HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 4; i < 12; i++)
                sb.Append(hashValue[i].ToString("x2"));
            return sb.ToString();
        }

    }
}
