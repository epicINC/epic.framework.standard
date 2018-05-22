using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

namespace Epic.Security
{
    public static partial class Utility
    {

        public static class Hex
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return Epic.Converters.HexString.Encode(Cryptography.AzDG.Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.AzDG.Decrypt(Encoding.UTF8.GetBytes(key), Epic.Converters.HexString.Decode(value)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return Epic.Converters.HexString.Encode(Cryptography.XXTEA.Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.XXTEA.Decrypt(Encoding.UTF8.GetBytes(key), Epic.Converters.HexString.Decode(value)));
            }

            #endregion
        }

        public static class Base64
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.AzDG.Encrypt(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(key)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.AzDG.Decrypt(Convert.FromBase64String(value), Encoding.UTF8.GetBytes(key)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.XXTEA.Encrypt(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(key)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.XXTEA.Decrypt(Convert.FromBase64String(value), Encoding.UTF8.GetBytes(key)));
            }

            #endregion

        }






    }
}
