using System;
using Homework_4.Individual;

namespace Homework_4.Crossover.DoubleCrossover
{
    public class ArithmeticCrossover : ICrossover<double[]>
    {
        public Individual<double[]> Cross(Individual<double[]> a, Individual<double[]> b)
        {
            var random = new Random();
            var dimension = a.Representation.Length;
            var child = new DoubleIndividual(dimension);

            for (var i = 0; i < dimension; i++)
            {
                var nextDouble = random.NextDouble();
                child.Representation[i] = nextDouble * a.Representation[i] + (1 - nextDouble) * b.Representation[i];
            }

            return child;
        }
    }
}