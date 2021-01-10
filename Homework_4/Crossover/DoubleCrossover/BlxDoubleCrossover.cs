using System;
using Homework_4.Individual;

namespace Homework_4.Crossover.DoubleCrossover
{
    public class BlxDoubleCrossover : ICrossover<double[]>
    {
        private static readonly Random Random = new Random();
        public Individual<double[]> Cross(Individual<double[]> a, Individual<double[]> b)
        {
            var dimension = a.Representation.Length;
            var child = new DoubleIndividual(dimension);
            const double alpha = 0.5;

            for (var i = 0; i < dimension; i++)
            {
                var min = Math.Min(a.Representation[i], b.Representation[i]);
                var max = Math.Max(a.Representation[i], b.Representation[i]);
                var difference = max - min;

                var lowerBound = min - difference * alpha;
                var upperBound = max + difference * alpha;

                child.Representation[i] = lowerBound + Random.NextDouble() * (upperBound - lowerBound);
            }

            return child;
        }
    }
}