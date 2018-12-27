using Epic.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DevTest
{
    class SymmetricAlgorithmTest
    {

        public static void Test()
        {

            AlgorithmTest<DESCryptoServiceProvider>();
            AlgorithmTest<RC2CryptoServiceProvider>();
            AlgorithmTest<RijndaelManaged>();
            AlgorithmTest<TripleDESCryptoServiceProvider>();

            AlgorithmTest<AesManaged>();
            TeaTest();

        }

        static string original = "id=62015836&n=张钢&c=北京昆仑亿发科技股份有限公司&p=董事长r=1541748819122";


        internal static void AlgorithmTest<T>() where T : SymmetricAlgorithm, new()
        {
            using (var crypto = new T())
            {
                crypto.GenerateKey();
                crypto.GenerateIV();
                byte[] encrypted = Encrypt<T>(original, crypto.Key, crypto.IV);
                string roundtrip = Decrypt<T>(encrypted, crypto.Key, crypto.IV);
                string hex = Epic.Converters.HexString.Encode(encrypted);
                string base64 = Convert.ToBase64String(encrypted);
                //Display the original data and the decrypted data.
                Console.WriteLine($"{typeof(T).Name}:");
                Console.WriteLine("Key:   {0}", crypto.Key.Length);
                Console.WriteLine("Byte:   {0}", encrypted.Length);
                Console.WriteLine("Hex:   {0}, {1}", hex.Length, hex);
                Console.WriteLine("Base64:   {0}, {1}", base64.Length, base64);
                Console.WriteLine("Decode: {0}", roundtrip);
                Console.WriteLine();
            }
        }

        static void TeaTest()
        {
            var key = Encoding.UTF8.GetBytes("288A339E65244CC995154A686C651B1E");
            var encrypted = XXTEA.Encrypt(key, Encoding.UTF8.GetBytes(original));
            string roundtrip = Encoding.UTF8.GetString(XXTEA.Decrypt(key, encrypted));
            string hex = Epic.Converters.HexString.Encode(encrypted);
            string base64 = Convert.ToBase64String(encrypted);
            //Display the original data and the decrypted data.
            Console.WriteLine($"Tea:");
            Console.WriteLine("Key:   {0}", key.Length);
            Console.WriteLine("Byte:   {0}", encrypted.Length);
            Console.WriteLine("Hex:   {0}, {1}", hex.Length, hex);
            Console.WriteLine("Base64:   {0}, {1}", base64.Length, base64);
            Console.WriteLine("Decode: {0}", roundtrip);
            Console.WriteLine();
        }
        


        internal static byte[] Encrypt<T>(string plainText, byte[] Key, byte[] IV) where T : SymmetricAlgorithm, new()
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (var algorithm = new T())
            {
                algorithm.Key = Key;
                algorithm.IV = IV;

                var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;

        }

        internal static string Decrypt<T>(byte[] cipherText, byte[] Key, byte[] IV) where T : SymmetricAlgorithm, new()
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (var algorithm = new T())
            {
                algorithm.Key = Key;
                algorithm.IV = IV;

                var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

    }
}
