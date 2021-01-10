using System;
using System.Linq;

namespace Homework_4.Individual
{
    public class BinaryIndividual : Individual<uint[][]>
    {
        
        public BinaryIndividual(int dimensions, double lowerBound, double upperBound, int precision)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Precision = precision;
            var n = FindNumberOfBits(lowerBound, upperBound, precision);
            N = n;
            Representation = new uint[dimensions][];

            for (var i = 0; i < dimensions; i++) 
                Representation[i] = GenerateRandomChromosome(n);
        }
        
        private static uint[] GenerateRandomChromosome(int size)
        {
            var random = new Random();

            var chromosome = new uint[size];

            for (var i = 0; i < size; i++) 
                chromosome[i] = (uint) random.Next(2);

            return chromosome;
        }

        // Clones other binary individual's values into himself.
        public BinaryIndividual(Individual<uint[][]> other)
        {
            Representation = other.Representation;
            Fitness = other.Fitness;
            N = other.N;
        }

        public override double[] ToDoubleArray()
        {
            var dimensions = Representation.GetLength(0);

            var intRepresentation = new int[dimensions];

            for (var i = 0; i < dimensions; i++) 
                intRepresentation[i] = ConvertToInt(Representation[i]);

            // var n = FindNumberOfBits(lowerBound, upperBound, precision);

            var result = new double[dimensions];

            for (var i = 0; i < dimensions; i++)
                result[i] = IntToDouble(intRepresentation[i], N, LowerBound, UpperBound);

            return result;
        }
        
        public static double IntToDouble(int representation, int n, double lowerBound, double upperBound)
        {
            var numerator = representation * (upperBound + lowerBound);
            var denominator = Math.Pow(2, n) - 1;

            return lowerBound + numerator / denominator;
        }

        public static int ConvertToInt(uint[] binary)
        {
            var value = 0;
            var powerOf2 = 1;
            for (var i = binary.Length - 1; i >= 0; i--) {
                if (binary[i] == 1) 
                    value += powerOf2;
                powerOf2 *= 2;
            }

            return value;
        }
        
        public static int FindNumberOfBits(double lowerBound, double upperBound, int precision)
        {
            var numerator = Math.Log(Math.Floor(1 + (upperBound - lowerBound) * Math.Pow(10, precision)));

            var denominator = Math.Log(2);

            var n = (int) Math.Ceiling(numerator / denominator);

            return n;
        }

        public static Tuple<int, int> DoubleToInt(double point, double lowerBound, double upperBound, int precision)
        {
            var n = FindNumberOfBits(lowerBound, upperBound, precision);
            var numerator = (point - lowerBound) * (Math.Pow(2, n) - 1);
            var denominator = upperBound - lowerBound;
            var binaryInt = Math.Ceiling(numerator / denominator);

            return new Tuple<int, int>((int)binaryInt, n);
        }
       
        // The [0]th element is the one with the highest weight.
        public static uint[] ConvertToUintArray(int binaryInt, int n, double lowerBound, double upperBound, int precision)
        {
            var result = new uint[n];

            for (var i = 0; i < n; i++)
            {
                result[i] = binaryInt % 2 == 1 ? (uint) 1 : 0;
                binaryInt /= 2;
            }

            return result.Reverse().ToArray();
        }
        
    }
    
}