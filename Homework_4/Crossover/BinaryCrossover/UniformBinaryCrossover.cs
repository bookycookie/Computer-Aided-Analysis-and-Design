using System;
using Homework_4.Individual;

namespace Homework_4.Crossover.BinaryCrossover
{
    public class UniformBinaryCrossover : ICrossover<uint[][]>
    {
        private static readonly Random Random = new Random();
        public Individual<uint[][]> Cross(Individual<uint[][]> a, Individual<uint[][]> b)
        {
            var child = new BinaryIndividual(a.Representation.Length, a.LowerBound, a.UpperBound, a.Precision);

            var dimensions = a.Representation.GetLength(0);
            var chromosomeLength = a.Representation[0].GetLength(0);

            for (var i = 0; i < dimensions; i++)
            {
                var randomChromosome = GenerateRandomChromosome(chromosomeLength);
                for (var j = 0; j < chromosomeLength; j++)
                    child.Representation[i][j] = a.Representation[i][j] * b.Representation[i][j] +
                                                 randomChromosome[j] * Xor(a.Representation[i][j],
                                                     b.Representation[i][j]);
            }

            return child;
        }

        private static uint[] GenerateRandomChromosome(int size)
        {
            var chromosome = new uint[size];

            for (var i = 0; i < size; i++) 
                chromosome[i] = (uint) Random.Next(2);

            return chromosome;
        }
        private static uint Xor(uint a, uint b) => (uint) (a == b ? 0 : 1);
    }
}