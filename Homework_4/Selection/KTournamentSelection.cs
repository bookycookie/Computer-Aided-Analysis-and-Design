using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Homework_4.Crossover;
using Homework_4.Individual;
using Homework_4.Mutation;

namespace Homework_4.Selection
{
    public class KTournamentSelection<T> : ISelection<T> where T : IEnumerable
    {
        private readonly Func<double[], double> _function;
        private readonly ICrossover<T> _crossover;
        private readonly IMutation<T> _mutation;
        private readonly int _lowerBound;
        private readonly int _upperBound;
        private readonly double _mutationProbability;
        private readonly int _k;
        private readonly int _precision;
        private readonly double _mean;
        private readonly double _deviation;
        private readonly bool _binary;

        private static readonly Random Random = new Random();

        public KTournamentSelection(Func<double[], double> function, ICrossover<T> crossover, IMutation<T> mutation,
            int lowerBound, int upperBound, double mutationProbability, int k, int precision, double mean,
            double deviation, bool binary = false)
        {
            _function = function;
            _crossover = crossover;
            _mutation = mutation;
            _lowerBound = lowerBound;
            _upperBound = upperBound;
            _mutationProbability = mutationProbability;
            _k = k;
            _precision = precision;
            _mean = mean;
            _deviation = deviation;
            _binary = binary;

        }

        public Individual<T> Select(List<Individual<T>> population)
        {

            var randomized = new int[_k];
            for (var i = 0; i < _k; i++)
            {
                var index = Random.Next(population.Count);
                randomized[i] = index;
            }

            randomized = randomized.OrderBy(i => population[i].Fitness).ToArray();
            
            var best = population[randomized[0]];
            var secondBest = population[randomized[1]];
            var worst = population[randomized[2]];
            
            var child = _crossover.Cross(best, secondBest);
            // A possible scientific breakthrough. The error was less than 1E-41 on 80 dimensions for F7.
            // _mutation.Mutate(child, _lowerBound, _upperBound, _mutationProbability, _mean, _deviation);
            _mutation.Mutate(child, _mean, _deviation, _lowerBound, _upperBound, _mutationProbability);
            child.Fitness = EvaluateIndividual(_function, child);

            
            if (child.Fitness > worst.Fitness) 
                population[randomized[2]] = child;
            
            // This yields much better results
            // my mind is telling me no
            // but my body.. my body is telling me yes
            if (child.Fitness > best.Fitness)
                population[randomized[0]] = child;

            return child;
        }

        private static double EvaluateIndividual(Func<double[], double> function, Individual<T> individual) =>
            -function(individual.ToDoubleArray());
    }
}