using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Epic.Security.Cryptography
{
    public class XXTEA2 : System.Security.Cryptography.SymmetricAlgorithm
    {
        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            throw new NotImplementedException();
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            throw new NotImplementedException();
        }

        public override void GenerateIV()
        {
            throw new NotImplementedException();
        }

        public override void GenerateKey()
        {
            throw new NotImplementedException();
        }


        public static unsafe byte[] Test(string value)
        {
            fixed(char* s = "testkey123456789")
            {
                uint* key = (uint*)s;
                fixed (char* buffer = value)
                {
                    EncryptBuffer(buffer, value.Length, key);
                    var result = new byte[value.Length * 2];
                    char* p = buffer;
                    var i = 0;
                    while(p < buffer + value.Length)
                    {
                        fixed (byte* ptr = &result[i])
                        {
                            *(short*)ptr = (short)*p;
                        }
                        p++;
                        i += 2;
                    }
                    return result;
                }
            }
        }


     
        static unsafe void EncryptTEA(uint* firstChunk, uint* secondChunk, uint* key)
        {
            uint y = *firstChunk;
            uint z = *secondChunk;
            uint sum = 0;

            uint delta = 0x9e3779b9; /* (sqrt(5)-1)/2*2^32 */

            for (int i = 0; i < 8; i++)//8轮运算(需要对应下面的解密核心函数的轮数一样)
            {
                sum += delta;
                y += ((z << 4) + key[0]) ^ (z + sum) ^ ((z >> 5) + key[1]);
                z += ((y << 4) + key[2]) ^ (y + sum) ^ ((y >> 5) + key[3]);
            }

            *firstChunk = y;
            *secondChunk = z;
        }

        static unsafe void DecryptTEA(uint* firstChunk, uint* secondChunk, uint* key)
        {
            uint sum = 0;
            uint y = *firstChunk;
            uint z = *secondChunk;
            uint delta = 0x9e3779b9;

            sum = delta << 3; //32轮运算，所以是2的5次方；16轮运算，所以是2的4次方；8轮运算，所以是2的3次方

            for (int i = 0; i < 8; i++) //8轮运算
            {
                z -= (y << 4) + key[2] ^ y + sum ^ (y >> 5) + key[3];
                y -= (z << 4) + key[0] ^ z + sum ^ (z >> 5) + key[1];
                sum -= delta;
            }
            *firstChunk = y;
            *secondChunk = z;
        }


        //buffer：输入的待加密数据buffer，在函数中直接对元数据buffer进行加密；size：buffer长度；key是密钥；
        public static unsafe void EncryptBuffer(char* buffer, int size, uint* key)
        {
            char* p = buffer;

            int leftSize = size;

            var i = 0;
            while (p < buffer + size && leftSize >= sizeof(uint) *2)
            {
                EncryptTEA((uint*)p, (uint*)(p + sizeof(uint)), key);
                p += sizeof(uint) * 2;
                leftSize -= sizeof(uint) * 2;
                i++;
            }

        }

        //buffer：输入的待解密数据buffer，在函数中直接对元数据buffer进行解密；size：buffer长度；key是密钥；
        public static unsafe void DecryptBuffer(char* buffer, int size, uint* key)
        {
            char* p = buffer;

            int leftSize = size;

            while (p < buffer + size && leftSize >= sizeof(uint) * 2)
            {
                DecryptTEA((uint*)p, (uint*)(p + sizeof(uint)), key);
                p += sizeof(uint) * 2;
                leftSize -= sizeof(uint) * 2;
            }
        }


    }
}
