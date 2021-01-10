using System.Collections;
using Homework_4.Individual;

namespace Homework_4.Crossover
{
    public interface ICrossover<T> where T : IEnumerable
    {
        public Individual<T> Cross(Individual<T> a, Individual<T> b);
    }
}