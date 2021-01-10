using System;
using System.Linq;

namespace Homework_4
{
    public static class Functions
    {
        public static Func<double[], double> F2 = x => Math.Pow(x[0] - 4, 2) + 4 * Math.Pow(x[1] - 2, 2);
        
        public static Func<double[], double> Rosenbrock = x =>
            100 * (x[1] - x[0] * x[0]) * (x[1] - x[0] * x[0]) + (1 - x[0]) * (1 - x[0]);

        public static Func<double[], double> F3 = x =>
        {
            double res = 0;
            for (var i = 1; i <= x.Length; i++) 
                res += (x[i - 1] - i) * (x[i - 1] - i);
            return res;
        };
        public static Func<double[], double> F6 = x =>
        {
            var squaredSum = x.Sum(t => t * t);

            var sinusFactor = Math.Sin(Math.Sqrt(squaredSum));

            var numerator = sinusFactor * sinusFactor - 0.5;

            var denominator = 1 + 0.001 * squaredSum;

            return 0.5 + numerator / (denominator * denominator);
        };

        public static Func<double[], double> F7 = x =>
        {
            var squaredSum = x.Sum(t => t * t);

            var firstFactor = Math.Pow(squaredSum, 0.25);

            var sinusFactor = Math.Sin(50 * Math.Pow(squaredSum, 0.1));
            var secondFactor = 1 + sinusFactor * sinusFactor;

            return firstFactor * secondFactor;
        };
    }
}