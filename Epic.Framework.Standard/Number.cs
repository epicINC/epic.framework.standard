using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic
{
    public class JNumber
    {
        public static IEnumerable<int> Random(int count)
        {
            var random = new Random();
            return Enumerable.Range(1, count).OrderBy(e => random.Next());
        }
    }
}
