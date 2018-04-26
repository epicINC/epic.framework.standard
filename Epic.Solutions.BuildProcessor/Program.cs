using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;

namespace Epic.Solutions.BuildProcessor
{
    class Program
    {
        static List<IProcessor> Tasks()
        {
            var result = new List<IProcessor>();
            result.Add(new EnumProcessor());
            return result;
        }

        static bool IsExists(string value)
        {
            return System.IO.File.Exists(value);
        }


        static void Run(string[] args)
        {

            //Console.WriteLine("args: "+ String.Join(",", args));
            string filePath = null, savePath = null;

            if (args.Length > 0)
            {
                if (!String.IsNullOrWhiteSpace(args[0]) && IsExists(args[0])) filePath = args[0];
                if (args.Length > 1 && !String.IsNullOrWhiteSpace(args[1])) savePath = args[1];
            }

            //Console.WriteLine("filePath: " + filePath);

            if (filePath == null) filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Epic.Framework.Standard.dll");
            if (!IsExists(filePath)) return;

            if (savePath == null) savePath = filePath +".mod";



            using (var assembly = AssemblyDefinition.ReadAssembly(filePath))
            {
                var module = assembly.MainModule;
                Tasks().ForEach(e => e.Process(module));
                assembly.Write(savePath);
            }


            //Console.WriteLine("savePath: " +  savePath);

        }


        #region Helper methods

        public static void Main(string[] args)
        {
            try
            {
                Run(args);
            }
            catch (Exception e)
            {
                string error = string.Format("---\nThe following error occurred while executing the snippet:\n{0}\n---", e.ToString());
                Console.WriteLine(error);
            }
            finally
            {
                Console.Write("done...");
                //Console.ReadKey();
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
