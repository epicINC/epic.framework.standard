using Epic.Components;
using Epic.Converters;
using Snowflake;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using Epic.Extensions;
using DevTest.Events;
using DevTest.Reflection;
using DevTest.Extensions;
using System.Text;

namespace DevTest
{
    [Flags]
    public enum DeviceType
    {
        /// <summary>
        /// 条码扫码仪
        /// </summary>
        CodeSanner = 1 << 1,

        /// <summary>
        /// IC读卡器
        /// </summary>
        ICCardReader = 1 << 2,

        /// <summary>
        /// 人脸识别
        /// </summary>
        Camera = 1 << 3,
        ManualInput

    }



    class Program
    {






        static void RunSnippet(string[] args)
        {

           // DevTest.JSON.JSONTester.LoadFromFile();
     

            //EventEmitterTest.Combine();
            Hardware.Printers.Jobs();
            return;

            SymmetricAlgorithmTest.Test();
            return;


            var text = "encode text valuefasdfasdfasdfaewe53253";
            var key = "testkey123456789";
            // TExuOaf8aXJsfZE6ojWJZXKiS0jQri8/iNIj/4omWRB4Jurkm7mG7eljT94=
            var result = Epic.Security.Cryptography.XXTEA.Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(text));
            WL(result.Length);
            WL(Convert.ToBase64String(result));

            var result1 = Epic.Security.Cryptography.XXTEA2.Test(text);
            WL(Convert.ToBase64String(result1));
            //SymmetricAlgorithmTest.Test();

        }

        #region Helper methods

        public static void Main(string[] args)
        {
            try
            {
                do
                {
                    RunSnippet(args);
                } while ((Console.ReadKey().Key != ConsoleKey.Escape));
                   
            }
            catch (Exception e)
            {
                string error = string.Format("---\nThe following error occurred while executing the snippet:\n{0}\n---", e.ToString());
                Console.WriteLine(error);
            }
            finally
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void WL(object text, params object[] args)
        {
            Console.WriteLine(text?.ToString(), args);
        }

        private static void RL()
        {
            Console.ReadLine();
        }

        private static void Break()
        {
            System.Diagnostics.Debugger.Break();
        }

        #endregion

    }
}
