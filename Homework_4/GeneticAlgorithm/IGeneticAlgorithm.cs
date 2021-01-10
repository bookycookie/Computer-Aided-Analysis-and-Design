using System;
using System.Collections;
using Homework_4.Crossover;
using Homework_4.Individual;
using Homework_4.Mutation;
using Homework_4.Selection;

namespace Homework_4.GeneticAlgorithm
{
    public interface IGeneticAlgorithm<T> where T : IEnumerable
    {
        public Individual<T> FindBestIndividual(bool speedRun, Func<double[], double> function, ISelection<T> selection,
            ICrossover<T> crossover, IMutation<T> mutation, double epsilon = 10E-6);
    }
}