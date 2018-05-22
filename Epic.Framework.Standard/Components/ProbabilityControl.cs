using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Components
{
    public class ProbabilityControl
    {
        // [1, 0, 1]
        public ProbabilityControl(double[] probabilities)
        {
            var total = probabilities.Sum();
            if (total != 1)
            {
                for (var i = 0; i < probabilities.Length; i++)
                    probabilities[i] = probabilities[i] / total;
            }
            Init(probabilities);
        }

        void Init(double[] probabilities)
        {
            this.Probabilities = new double[probabilities.Length];

            double result = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                if (probabilities[0] == 0) continue;
                this.Probabilities[i] = result += probabilities[i];
            }
        }

        double[] Probabilities;


        public int Calc(double percent)
        {
            for(var i = 0; i < this.Probabilities.Length; i++)
            {
                if (this.Probabilities[i] == 0) continue; 
                if (percent <= this.Probabilities[i]) return i;
            }
            return -1;
        }

    }
}
