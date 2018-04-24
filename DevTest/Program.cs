using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTest
{
    class Program
    {
        static void Main(string[] args)
        {


            Print();

            Console.ReadLine();
        }


        static void Print()
        {
            var result = YieldDate();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.year}, { String.Join(",", item.months)}");
            }
        }

        static IEnumerable<(int year, IEnumerable<int> months)> YieldDate()
        {
            var value = new Dictionary<int, int[]>
            {
                {2015, new[]{1, 10 } },
                {2017, null },

            };
            return value.Select(e => (year: e.Key, months: Iteration(e.Value?[0] ?? 12, e.Value?[1] ?? 1)));
        }


        public static IEnumerable<int> Iteration(int start, int end)
        {
            if (start == end) return new[] { start };
            if (start > end) (start, end) = (end, start);
            return Enumerable.Range(start, end - start + 1).OrderByDescending(e => e);
        }
    }
}
