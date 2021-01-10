using System;
using System.Collections.Generic;
using Homework_4.Crossover;
using Homework_4.Individual;
using Homework_4.Mutation;
using Homework_4.Selection;

namespace Homework_4.GeneticAlgorithm
{
    public class BinaryGa : IGeneticAlgorithm<uint[][]>
    {
        
        public List<Individual<uint[][]>> Population;

        private readonly Func<double[], double> _function;

        private readonly int _iterations;

        private readonly int _dimensions;
        private readonly int _populationSize;
        private readonly double[] _populationRange;
        private readonly int _precision;

        private double _maxFitness = double.NegativeInfinity;
        private Individual<uint[][]> _maxIndividual;
        
        public BinaryGa(Func<double[], double> function, int dimensions, int populationSize, int iterations, double lowerBound, double upperBound, int precision)
        {
            _function = function;
            _dimensions = dimensions;
            _populationSize = populationSize;
            _iterations = iterations;
            _populationRange = new[] {lowerBound, upperBound};
            _precision = precision;
        }
        
        public Individual<uint[][]>FindBestIndividual(bool speedRun, Func<double[], double> function, ISelection<uint[][]> selection,
            ICrossover<uint[][]> crossover, IMutation<uint[][]> mutation, double epsilon = 10E-6)
        {
            var lowerBound = _populationRange[0];
            var upperBound = _populationRange[1];
            Population = InitializePopulation(_dimensions, _populationSize, lowerBound, upperBound, _precision);
            var i = 0;
            do
            {
                var child = selection.Select(Population);

                if (child.Fitness > _maxFitness) 
                {
                    _maxFitness = child.Fitness;
                    _maxIndividual = child;
                    // Console.WriteLine($"i: {i} ---- {_maxFitness}");
                }

                if (speedRun && Math.Abs(_maxFitness) < epsilon)
                {
                    Console.WriteLine($"Total iterations: {i:N0}");
                    return _maxIndividual;
                }

                i++;
            } while (i < _iterations);

            // return _maxFitness;
            Console.WriteLine($"Total iterations: {i:N0}");
            return _maxIndividual;
        }

        

        private List<Individual<uint[][]>> InitializePopulation(in int dimensions, in int populationSize, in double lowerBound, in double upperBound, in int precision)
        {
            var population = new List<Individual<uint[][]>>(populationSize);

            for (var i = 0; i < populationSize; i++)
            {
                population.Add(new BinaryIndividual(dimensions, lowerBound, upperBound, precision));
                var doubleBinaryValue = population[i].ToDoubleArray();
                population[i].Fitness = -_function(doubleBinaryValue);
                if (population[i].Fitness > _maxFitness)
                {
                    _maxFitness = population[i].Fitness;
                    _maxIndividual = population[i];
                }   
            }
            
            return population;
        }
    }
}