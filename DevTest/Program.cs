using Epic.Components;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var pc = new ProbabilityControl(new double[] { 0.1, 0, 0.2 });


            WL(pc.Calc(0.1) >= 0);


        }



        #region Helper methods

        public static void Main(string[] args)
        {
            try
            {
                RunSnippet(args);
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
