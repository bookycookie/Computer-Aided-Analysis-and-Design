using System;

namespace Homework_4
{
    public static class RandomExtension
    {
        public static double NextGaussian(this Random random, double mean, double stdDev)
        {
            var u1 = 1.0 - random.NextDouble();
            var u2 = 1.0 - random.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * randStdNormal;
        }

        public static double NextDouble(this Random random, double lowerBound, double upperBound) =>
            lowerBound + random.NextDouble() * (upperBound - lowerBound);
    }
}