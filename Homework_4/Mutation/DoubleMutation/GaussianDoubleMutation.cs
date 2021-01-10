using System;
using Homework_4.Individual;

namespace Homework_4.Mutation.DoubleMutation
{
    public class GaussianDoubleMutation : IMutation<double[]>
    {
        private static readonly Random Random = new Random();
        public void Mutate(Individual<double[]> individual, double mean, double deviation, double lowerBound, double upperBound, double mutationProbability)
        {
            
            var length = individual.Representation.Length;
            for (var i = 0; i < length; i++) {
                var next = Random.NextDouble();
                if (next < mutationProbability) 
                    individual.Representation[i] += Random.NextGaussian(mean, deviation);
            }
            
        }
    }
}