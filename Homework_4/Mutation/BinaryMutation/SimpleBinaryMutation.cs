using System;
using Homework_4.Individual;

namespace Homework_4.Mutation.BinaryMutation
{
    public class SimpleBinaryMutation : IMutation<uint[][]>
    {
        private static readonly Random Random = new Random();
        public void Mutate(Individual<uint[][]> individual, double mean, double deviation, double lowerBound, double upperBound,
            double mutationProbability)
        {
            var dimensions = individual.Representation.GetLength(0);
            var chromosomeLength = individual.Representation[0].GetLength(0);

            var chromosomeMutationProbability = 1 - Math.Pow(1 - mutationProbability, chromosomeLength);

            for (var i = 0; i < dimensions; i++)
            {
                var nextDouble = Random.NextDouble();
                if (nextDouble <= chromosomeMutationProbability)
                    for (var j = 0; j < chromosomeLength; j++)
                    {
                        if (Random.NextDouble() < mutationProbability)
                            individual.Representation[i][j] = (uint) Random.Next(2);
                    }
            }

        }
    }
}