using Epic.Components;
using Epic.Converters;
using Snowflake;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;

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
