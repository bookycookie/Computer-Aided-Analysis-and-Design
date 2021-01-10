using System.Collections;
using System.Collections.Generic;
using Homework_4.Individual;

namespace Homework_4.Selection
{
    public interface ISelection<T> where T : IEnumerable
    {
        public Individual<T> Select(List<Individual<T>> population);
        
    }
}