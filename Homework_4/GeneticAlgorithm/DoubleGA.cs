using System;
using System.Collections.Generic;
using Homework_4.Crossover;
using Homework_4.Individual;
using Homework_4.Mutation;
using Homework_4.Selection;

namespace Homework_4.GeneticAlgorithm
{
    public class DoubleGa : IGeneticAlgorithm<double[]>
    {
        public List<Individual<double[]>> Population;

        private readonly Func<double[], double> _function;

        private readonly int _iterations;

        private readonly int _dimensions;
        private readonly int _populationSize;
        private readonly double[] _populationRange;

        private double _maxFitness = double.NegativeInfinity;
        private Individual<double[]> _maxIndividual;


        public DoubleGa(Func<double[], double> function, int dimensions, int populationSize, int iterations, double lowerBound, double upperBound)
        {
            _function = function;
            _dimensions = dimensions;
            _populationSize = populationSize;
            _iterations = iterations;
            _populationRange = new[] {lowerBound, upperBound};
        }

        public Individual<double[]> FindBestIndividual(bool speedRun, Func<double[], double> function, ISelection<double[]> selection,
            ICrossover<double[]> crossover, IMutation<double[]> mutation, double epsilon = 10E-6)
        {
            var lowerBound = _populationRange[0];
            var upperBound = _populationRange[1];
            Population = InitializePopulation(_dimensions, _populationSize, lowerBound, upperBound);
            var i = 0;
            do
            {
                var child = selection.Select(Population);

                if (child.Fitness > _maxFitness) 
                {
                    _maxFitness = child.Fitness;
                    _maxIndividual = child;
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

        private List<Individual<double[]>> InitializePopulation(int dimensions, int populationSize,
            double lowerBound, double upperBound)
        {
            var random = new Random();
            var population = new List<Individual<double[]>>(populationSize);

            for (var i = 0; i < populationSize; i++)
            {
                population.Add(new DoubleIndividual(dimensions));
                for (var j = 0; j < dimensions; j++)
                {
                    population[i].Representation[j] = random.NextDouble(lowerBound, upperBound);
                    
                }
                population[i].Fitness = -_function(population[i].Representation);
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