using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Security.Cryptography
{


    /// <summary>
    /// Author:		jIAn-zhoung 
    /// Description:	
    /// </summary>
    /// <history>
    ///	Url:    http://jian-zhoung.blogspot.com/2008/02/azdg-for-c.html
    /// Modify: SLIGHTBOY 2008-3-5 13:15;
    /// </history>
    public static class AzDG
    {
        static Encoding chaSset = Encoding.UTF8;

        public static byte[] PrivateKeyCrypt(byte[] key, byte[] data)
        {
            if (key.Length < 32)
            {
                byte[] fullKey = new byte[32];

                Array.Copy(key, fullKey, key.Length);
                //for (int i = key.Length; i < 32; i++)
                //    fullKey[i] = (byte)(i % 2) ;
                key = fullKey;
            }
            byte round = 0;
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                result[i] = (byte)(data[i] ^ key[round++]);
                if (round > 30) round = 0;
            }
            return result;
        }

        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            byte round = 0;
            byte[] crcKey = chaSset.GetBytes(Utility.Salt(4));
            if (crcKey.Length < 32)
            {
                byte[] fullKey = new byte[32];
                Array.Copy(crcKey, fullKey, crcKey.Length);
                crcKey = fullKey;
            }


            byte[] result = new byte[data.Length * 2];
            for (int i = 0, j = 0; i < data.Length; ++i, ++j)
            {
                result[j] = crcKey[round];
                ++j;
                result[j] = (byte)(data[i] ^ crcKey[round++]);
                if (round > 30) round = 0;
            }
            return PrivateKeyCrypt(result, key);
        }


        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            data = PrivateKeyCrypt(data, key);
            byte[] result = new byte[(int)(data.Length / 2)];
            for (int i = 0, j = 0; i < data.Length; ++i, ++j)
            {
                result[j] = (byte)(data[i] ^ data[++i]);
            }
            return result;
        }


        /*
        public static byte[] PrivateKeyCrypt(byte[] data, string key)
        {
            byte round = 0;
            byte[] encryptKey = chaSset.GetBytes(Utility.Cryptography.MD5(key));
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                if (round > 31) round = 0;
                result[i] = (byte)(data[i] ^ encryptKey[round++]);
            }
            return result;
        }


        public static byte[] Encrypt(byte[] data, string key)
        {
            byte round = 0;
            byte[] crcKey = chaSset.GetBytes(Utility.Cryptography.MD5(Utility.Cryptography.GenerateSalt(4)));
            byte[] result = new byte[data.Length * 2];
            for (int i = 0, j = 0; i < data.Length; ++i, ++j)
            {
                if (round > 31) round = 0;
                result[j] = crcKey[round];
                ++j;
                result[j] = (byte)(data[i] ^ crcKey[round++]);
            }
            return PrivateKeyCrypt(result, key);
        }


        public static byte[] Decrypt(byte[] data, string key)
        {
            data = PrivateKeyCrypt(data, key);
            byte[] result = new byte[(int)(data.Length / 2)];
            for (int i = 0, j = 0; i < data.Length; ++i, ++j)
            {
                result[j] = (byte)(data[i] ^ data[++i]);
            }
            return result;
        }
        */
    }

}
