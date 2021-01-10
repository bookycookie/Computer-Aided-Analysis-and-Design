using System;
using System.Collections.Generic;
using System.Linq;
using Homework_4.Individual;

namespace Homework_4.Crossover.BinaryCrossover
{
    public class SinglePointBinaryCrossover : ICrossover<uint[][]>
    {
        private static readonly Random Random = new Random();

        public Individual<uint[][]> Cross(Individual<uint[][]> a, Individual<uint[][]> b)
        {
            var child = new BinaryIndividual(a.Representation.Length, a.LowerBound, a.UpperBound, a.Precision);
            var genes = a.Representation.Length;
            var bits = b.Representation[0].Length;

            for (var i = 0; i < genes; i++)
            {
                var parentGene1 = a.Representation[i];
                var parentGene2 = b.Representation[i];

                var crossoverPoint = Random.Next(bits - 1);

                for (var bit = 0; bit < bits; bit++)
                    child.Representation[i][bit] = (bit <= crossoverPoint ? parentGene1[bit] : parentGene2[bit]);
            }

            return child;
        }


        // var child = new BinaryIndividual(a);
            // var dimensions = a.Representation.GetLength(0);
            //
            // for (var i = 0; i < dimensions; i++)
            //     child.Representation[i] = SinglePointCross(a.Representation[i], b.Representation[i]);
            //
            // return child;
    

    private static uint[] SinglePointCross(IReadOnlyCollection<uint> a, IEnumerable<uint> b)
        {
            var length = a.Count;

            var singlePoint = Random.Next(length);

            var firstPart = a.Take(singlePoint);
            var secondPart = b.Skip(singlePoint).Take(length - singlePoint);

            var crossed = firstPart.Concat(secondPart).ToArray();

            return crossed;
        }
    }
}