using System.Collections;
using Homework_4.Individual;

namespace Homework_4.Mutation
{
    public interface IMutation<T> where T : IEnumerable
    {
        public void Mutate(Individual<T> individual, double mean, double deviation, double lowerBound, double upperBound, double mutationProbability);
    }
}