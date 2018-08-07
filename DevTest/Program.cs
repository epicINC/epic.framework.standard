using Epic.Components;
using Epic.Converters;
using Snowflake;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using Epic.Extensions;
namespace DevTest
{
 

    class Program
    {



        public class User
        {
            public int ID { get; set; }
            public string Name { get; set; }


        }
        static void RunSnippet(string[] args)
        {
            var client = new System.Net.Http.HttpClient();

            using (var fs = new System.IO.FileStream(@"E:\eastfair\20180619 Hardware\doc\工作人员_照片\test.jpg", System.IO.FileMode.Open))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    var result = client.PostFormAsync("http://localhost:9896/", new { stream = ms });
                    WL(result);
                }

            }

            


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
            Console.WriteLine(text.ToString(), args);
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
