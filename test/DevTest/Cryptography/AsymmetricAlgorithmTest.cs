using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.IO;
using Org.BouncyCastle.Asn1.Anssi;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement.Kdf;

namespace DevTest
{
    public class AsymmetricAlgorithmTest
    {
        static string original = "id=62015836&n=张钢&c=北京昆仑亿发科技股份有限公司&p=董事长&=a=中国 北京+Beijing China&i=专业刊物/网络媒体+Publication/ Online media&r=5554";

        public static void Test()
        {

            var key = CreateKey();
            var encrypted = Encode(original, key.Public);
            //var roundtrip = Decode(encrypted, key.Private);
            string hex = Epic.Converters.HexString.Encode(encrypted);
            string base64 = Convert.ToBase64String(encrypted);
            Console.WriteLine("key:   {0}", key.Private.Exponent.Length);
            Console.WriteLine("Byte:   {0}", encrypted.Length);
            Console.WriteLine("Hex:   {0}, {1}", hex.Length, hex);
            Console.WriteLine("Base64:   {0}, {1}", base64.Length, base64);
            //Console.WriteLine("Decode: {0}", roundtrip);
        }

        static Func<byte[], string> To = Convert.ToBase64String;
        static Func<string, byte[]> From = Convert.FromBase64String;


        static (RSAParameters Public, RSAParameters Private) CreateKey(RSA algorithm = null)
        {
            if (algorithm != null) return GenerateKey(algorithm);
            using (algorithm = RSA.Create())
            {
                return GenerateKey(algorithm);
            }
        }

        static (RSAParameters Public, RSAParameters Private) GenerateKey(RSA algorithm)
        {
#if DEBUG
            Console.WriteLine(string.Join(String.Empty, algorithm.LegalKeySizes.Select(e => String.Format("MinSize:{0} MaxSize:{1} SkipSize:{2}", e.MinSize, e.MaxSize, e.SkipSize))));
#endif
            algorithm.KeySize = 512;
            return (Public: algorithm.ExportParameters(false), Private: algorithm.ExportParameters(true));
        }


        static byte[] Encode(string value, RSAParameters publicKey)
        {
            return Encode(Encoding.UTF8.GetBytes(value), publicKey);
        }

        static byte[] Encode(byte[] value, RSAParameters publicKey)
        {
            using (var algorithm = RSA.Create())
            {
                algorithm.ImportParameters(publicKey);
                var maxBlockSize = algorithm.KeySize / 8 - 11;
                if (value.Length <= maxBlockSize)
                    return algorithm.Encrypt(value, RSAEncryptionPadding.Pkcs1);


                using (var plain = new MemoryStream(value))
                {
                    using (var cryp = new MemoryStream())
                    {
                        byte[] buffer = new byte[maxBlockSize];
                        int blockSize = plain.Read(buffer, 0, maxBlockSize);

                        while (blockSize > 0)
                        {
                            byte[] toEncrypt = new byte[blockSize];
                            Array.Copy(buffer, 0, toEncrypt, 0, blockSize);

                            byte[] cryptograph = algorithm.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);
                            cryp.Write(cryptograph, 0, cryptograph.Length);

                            blockSize = plain.Read(buffer, 0, maxBlockSize);
                        }

                        return cryp.ToArray();
                    }
                }
            }
        }


        static string Decode(string value, RSAParameters privateKey)
        {
            return Convert.ToBase64String(Decode(Convert.FromBase64String(value), privateKey));
        }

        static byte[] Decode(byte[] value, RSAParameters privateKey)
        {
            using (var algorithm = RSA.Create())
            {
                algorithm.ImportParameters(privateKey);
                return algorithm.Decrypt(value, RSAEncryptionPadding.Pkcs1);
            }
        }




        static void ECCTest()
        {
            string curveName = "P-521";
            var ecP1 = AnssiNamedCurves.GetByName("FRP256v1");
            var ecP21 = TeleTrusTNamedCurves.GetByName("brainpoolp512t1");
            var ecP = NistNamedCurves.GetByName(curveName);
            var random = new SecureRandom();

            var eCDomainParameters = new ECDomainParameters(ecP.Curve, ecP.G, ecP.N, ecP.H, ecP.GetSeed());
            var pGen = new ECKeyPairGenerator();
            var genParam = new ECKeyGenerationParameters(
                eCDomainParameters,
                random);
            pGen.Init(genParam);
            var asymmetricCipherKeyPair = pGen.GenerateKeyPair();

            var senderPrivate = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D.ToByteArray();
            var senderPublic = ((ECPublicKeyParameters)asymmetricCipherKeyPair.Public).Q.GetEncoded();

            var asymmetricCipherKeyPairA = pGen.GenerateKeyPair();

            var recieverPrivate = ((ECPrivateKeyParameters)asymmetricCipherKeyPairA.Private).D.ToByteArray();

            var recieverPublic = ((ECPublicKeyParameters)asymmetricCipherKeyPairA.Public).Q.GetEncoded();

            var sharedSecret = GetSharedSecretValue(asymmetricCipherKeyPair, asymmetricCipherKeyPairA);

            var deriveSecret = DeriveSymmetricKeyFromSharedSecret(sharedSecret);

            var encrypted = Encrypt(Encoding.UTF8.GetBytes(original), deriveSecret);
            var roundtrip = Encoding.UTF8.GetString(Decrypt(encrypted, deriveSecret));
            string hex = Epic.Converters.HexString.Encode(encrypted);
            string base64 = Convert.ToBase64String(encrypted);
            Console.WriteLine("Byte:   {0}", encrypted.Length);
            Console.WriteLine("Hex:   {0}, {1}", hex.Length, hex);
            Console.WriteLine("Base64:   {0}, {1}", base64.Length, base64);
            Console.WriteLine("Decode: {0}", roundtrip);
        }

        static byte[] GetSharedSecretValue(AsymmetricCipherKeyPair asymmetricCipherKeyPair, AsymmetricCipherKeyPair asymmetricCipherKeyPairA, bool isEncrypt = true)
        {
            var eLacAgreement = new ECDHCBasicAgreement();
            eLacAgreement.Init(asymmetricCipherKeyPair.Private);
            ECDHCBasicAgreement acAgreement = new ECDHCBasicAgreement();
            acAgreement.Init(asymmetricCipherKeyPairA.Private);
            var eLA = eLacAgreement.CalculateAgreement(asymmetricCipherKeyPairA.Public);
            var a = acAgreement.CalculateAgreement(asymmetricCipherKeyPair.Public);
            if (eLA.Equals(a) && !isEncrypt)
            {
                return eLA.ToByteArray();
            }
            if (eLA.Equals(a) && isEncrypt)
            {
                return a.ToByteArray();
            }
            return null;
        }

        static byte[] DeriveSymmetricKeyFromSharedSecret(byte[] sharedSecret)
        {
            var egH = new ECDHKekGenerator(DigestUtilities.GetDigest("SHA256"));
 
            egH.Init(new DHKdfParameters(NistObjectIdentifiers.Aes, sharedSecret.Length, sharedSecret));
            byte[] symmetricKey = new byte[DigestUtilities.GetDigest("SHA256").GetDigestSize()];
            egH.GenerateBytes(symmetricKey, 0, symmetricKey.Length);

            return symmetricKey;
        }


        static byte[] Encrypt(byte[] data, byte[] derivedKey)
        {
            byte[] output = null;
            try
            {
                var keyparam = ParameterUtilities.CreateKeyParameter("DES", derivedKey);
                var cipher = CipherUtilities.GetCipher("DES/ECB/ISO7816_4PADDING");
                cipher.Init(true, keyparam);
                try
                {
                    output = cipher.DoFinal(data);
                    return output;
                }
                catch (System.Exception ex)
                {
                    throw new CryptoException("Invalid Data");
                }
            }
            catch (Exception ex)
            {

            }

            return output;
        }

        static byte[] Decrypt(byte[] cipherData, byte[] derivedKey)
        {
            byte[] output = null;
            try
            {
                KeyParameter keyparam = ParameterUtilities.CreateKeyParameter("DES", derivedKey);
                IBufferedCipher cipher = CipherUtilities.GetCipher("DES/ECB/ISO7816_4PADDING");
                cipher.Init(false, keyparam);
                try
                {
                    output = cipher.DoFinal(cipherData);

                }
                catch (System.Exception ex)
                {
                    throw new CryptoException("Invalid Data");
                }
            }
            catch (Exception ex)
            {
            }

            return output;
        }
    }
}
