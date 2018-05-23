using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Epic.Components
{


    public class ProbabilityControlSuit<T>
    {
        public double Rate { get; set; }
        public bool Fix { get; set; }


        public T Award { get; set; }
    }


    public class ProbabilityControl
    {

        /// <summary>
        /// 固定比例，不自动计算
        /// </summary>
        /// <param name="probabilities"></param>
        /// <returns></returns>
        public static ProbabilityControl Fix(double[] value)
        {
            return new ProbabilityControl(Init(value));
        }

        public static ProbabilityControl Calc(double[] value)
        {
            return new ProbabilityControl(Init(CalcRate(value)));
        }

        static double[] CalcRate(double[] value)
        {
            var total = value.Sum();
            if (total != 1)
            {
                for (var i = 0; i < value.Length; i++)
                    value[i] = value[i] / total;
            }
            return value;
        }

        static double[] Init(double[] value)
        {
            var result = new double[value.Length];

            double total = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == 0) continue;
                result[i] = total += value[i];
            }
            return result;
        }

        // [1, 0, 1]
        ProbabilityControl(double[] value)
        {
            this.Probabilities = value;
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
