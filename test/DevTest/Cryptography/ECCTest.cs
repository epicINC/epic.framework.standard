using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace DevTest
{
    public class ECCTest
    {
        static string original = "id=62015836&n=张钢&c=北京昆仑亿发科技股份有限公司&p=董事长&=a=中国 北京+Beijing China&i=专业刊物/网络媒体+Publication/ Online media&r=5554";


        public static void Test()
        {
            ECDiffieHellmanCng alice = new ECDiffieHellmanCng();
            alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            alice.HashAlgorithm = CngAlgorithm.Sha1;


            ECDiffieHellmanCng bob = new ECDiffieHellmanCng();
            bob.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            bob.HashAlgorithm = CngAlgorithm.Sha1;



            byte[] aliceKey = alice.DeriveKeyMaterial(bob.PublicKey);
            byte[] bobKey = bob.DeriveKeyMaterial(alice.PublicKey);
            

            Console.WriteLine(bobKey.SequenceEqual(aliceKey));

            var r = new RijndaelManaged();
            r.GenerateIV();
            var iv = r.IV;

            byte[] encrypted = SymmetricAlgorithmTest.Encrypt<RijndaelManaged>(original, bobKey, iv);
            string roundtrip = SymmetricAlgorithmTest.Decrypt<RijndaelManaged>(encrypted, aliceKey, iv);
            string hex = Epic.Converters.HexString.Encode(encrypted);
            string base64 = Convert.ToBase64String(encrypted);
            //Display the original data and the decrypted data.
            Console.WriteLine($"ECC:");
            Console.WriteLine("Byte:   {0}", encrypted.Length);
            Console.WriteLine("Hex:   {0}, {1}", hex.Length, hex);
            Console.WriteLine("Base64:   {0}, {1}", base64.Length, base64);
            Console.WriteLine("Decode: {0}", roundtrip);
            Console.WriteLine();

        }

    }
}
