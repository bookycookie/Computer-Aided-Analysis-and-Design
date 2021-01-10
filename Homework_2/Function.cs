using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Homework_2
{
    public static class Function
    {
        public static double F1(double x1, double x2) => Math.Pow(100 * (x2 - Math.Pow(x1, 2)), 2) + Math.Pow(1 - x1, 2);

        public static double F2(double x1, double x2) => Math.Pow(x1 - 4, 2) + 4 * Math.Pow(x2 - 2, 2);

        public static double F3(IEnumerable<double> x)
        {
            double result = 0;

            var i = 0;
            foreach (var xi in x)
            {
                result += Math.Pow(xi - i, 2);
                i++;
            }

            return result;
        }
            

        public static double F4(double x1, double x2) => Math.Abs((x1 - x2) * (x1 + x2)) + Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(x2, 2));

        public static double F6(IEnumerable<double> x)
        {
            var sum = x.Sum(xi => Math.Pow(xi, 2));

            var result = x.Sum(xi => 0.5 + (Math.Pow(Math.Sin(Math.Sqrt(sum)), 2) - 0.5 / Math.Pow(1 + 0.001 * sum, 2)));

            return result;
        }
    }
}