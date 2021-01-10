using System;
using Homework_4.Crossover;
using Homework_4.Crossover.BinaryCrossover;
using Homework_4.Crossover.DoubleCrossover;
using Homework_4.GeneticAlgorithm;
using Homework_4.Individual;
using Homework_4.Mutation;
using Homework_4.Mutation.BinaryMutation;
using Homework_4.Mutation.DoubleMutation;
using Homework_4.Selection;

namespace Homework_4
{
    class Program
    {
        private const int LowerBound = -50;
        private const int UpperBound = 150;
        private const double Mean = 0.0;
        private const double Deviation = 0.01;
        private const double Epsilon = 10E-6;

        static void Main(string[] args)
        {
            // Binary Rosenbrock 100%: precision: 6, iterations: 1_000_000, isBinary: true, mutationProbability: 0.15, populationSize: 250 uniform x
            // Binary F3 100% ^ uniform x
            // Binary F6 100% ^ uniform x
            // Binary F7 100% 0% -- best error 3,628198E-004
            // TaskRepeat(false, 10, precision: 7, iterations: 1_000_000, isBinary: true, mutationProbability: 0.04, populationSize: 250);
            // Assignment1(false);
            // Assignment2(isBinary: false, precision: 6, iterations: 1_000_000, mutationProbability: 0.15, populationSize: 250);
            // Assignment3();
            Assignment4();
            // Assignment5();
            // var binaryTest = new BinaryIndividual(2, -50, 150, 5);
            // Console.WriteLine($"{string.Join(" BLA ", binaryTest.ToDoubleArray())}");
        }

        private static void Assignment1(bool isBinary = false, bool speedRun = true,
            int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            Console.WriteLine("--------------ASSIGNMENT1--------------");
            Rosenbrock(isBinary: isBinary, speedRun: speedRun);
            F3(isBinary: isBinary, speedRun: speedRun);
            F6(isBinary: isBinary, speedRun: speedRun);
            F7(isBinary: isBinary, speedRun: speedRun);
            Console.WriteLine("-----------------END--------------");
        }

        private static void Assignment2(bool isBinary = false, bool speedRun = true,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            var dimensions = new[] {1, 3, 6, 10};
            Console.WriteLine("--------------ASSIGNMENT2--------------");
            foreach (var dimension in dimensions)
            {
                F6(isBinary: false, speedRun: speedRun, dimensions: dimension);
                F7(isBinary: false, speedRun: speedRun, dimensions: dimension);
            }

            Console.WriteLine("-----------------END--------------");
        }

        private static void Assignment3(bool isBinary = false, bool speedRun = true,
            int populationSize = 150,
            int iterations = 1000000,
            double mutationProbability = 0.15, int precision = 4, int k = 3, double deviation = 0.01)
        {
            var dimensions = new[] {3, 6};
            var totalEvaluations = iterations + populationSize;
            Console.WriteLine("--------------ASSIGNMENT3--------------");
            foreach (var dimension in dimensions)
            {
                F6(isBinary: false, iterations: totalEvaluations, speedRun: speedRun, dimensions: dimension, populationSize: 300,
                    mutationProbability: 0.05, deviation: 0.5);
                F6(isBinary: true, iterations: totalEvaluations, speedRun: speedRun, dimensions: dimension, populationSize: 300,
                    mutationProbability: 0.05, deviation: 0.5);
                F7(isBinary: false, iterations: totalEvaluations, speedRun: speedRun, dimensions: dimension, mutationProbability: 0.15,
                    deviation: 0.01);
                F7(isBinary: true, iterations: totalEvaluations, speedRun: speedRun, dimensions: dimension, mutationProbability: 0.15,
                    deviation: 0.01);
            }

            Console.WriteLine("-----------------END--------------");
        }

        private static void Assignment4(bool isBinary = false, bool speedRun = true,
            int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            var populationSizes = new[] {30, 50, 100, 200};
            var mutationProbabilities = new[] {0.1, 0.3, 0.6, 0.9};
            Console.WriteLine("--------------ASSIGNMENT4--------------");
            // Best size = 100
            // foreach (var size in populationSizes)
            //     F6(isBinary, speedRun, dimensions, size, iterations, mutationProbability,
            // precision, k, deviation);

            // Best mutation probability = 30%
            // foreach (var probability in mutationProbabilities)
            //     F6(isBinary, speedRun, dimensions, populationSize, iterations, probability,
            // precision, k, deviation);
            
            //populationSize: 100, iterations: 100_000, dimensions: 2, mutationProbability: 0.3, deviation: 1.0
            F6(isBinary, speedRun, dimensions, populationSize, iterations, mutationProbability,
                precision, k, deviation);
            Console.WriteLine("-----------------END--------------");
        }

        private static void Assignment5(bool isBinary = false, bool speedRun = true,
            int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            var ks = new[] {3, 4, 5, 6, 7, 9, 10};
            Console.WriteLine("--------------ASSIGNMENT5--------------");
            // foreach (var k in ks) 
            //     F6(isBinary: isBinary, speedRun: speedRun, k: k);

            foreach (var kx in ks)
                F7(isBinary, speedRun, dimensions, populationSize, iterations, mutationProbability,
                    precision, kx, deviation);
            Console.WriteLine("-----------------END--------------");
        }

        public static void TestingArrayConversions()
        {
            var points = new double[] {17.5, 17.7};
            uint[][] pointerinos = new uint[2][];
            var intArray = new int[2];

            var n = new int[2];

            for (int i = 0; i < 2; i++)
            {
                (intArray[i], n[i]) = BinaryIndividual.DoubleToInt(points[i], 0, 63, 2);
            }

            for (int i = 0; i < 2; i++)
            {
                pointerinos[i] = BinaryIndividual.ConvertToUintArray(intArray[i], n[i], 0, 63, 2);
            }

            var converted = new double[2];

            // var bindividual = new BinaryIndividual(2, 0, 63, 2);

            Console.WriteLine(string.Join(", ", converted));
            Console.WriteLine(string.Join(" . ", n));
        }

        private static void DoubleBinaryDoubleConversion()
        {
            // double[] point = new[] { 2.0} ;
            // BinaryIndividual ind = new BinaryIndividual();
            var (integer, numOfBits) = BinaryIndividual.DoubleToInt(17.5, lowerBound: 0, upperBound: 63, precision: 3);
            Console.WriteLine(integer);

            var uintArray = BinaryIndividual.ConvertToUintArray(integer, numOfBits, 0, 63, 3);
            var returnString = "[";
            foreach (var u in uintArray)
                returnString += $"{u}";

            returnString += "]";
            Console.WriteLine(returnString);

            var testBackToInt = BinaryIndividual.ConvertToInt(uintArray);
            Console.WriteLine(testBackToInt);
            var testBackToOriginalPoint = BinaryIndividual.IntToDouble(testBackToInt, numOfBits, 0, 63);
            Console.WriteLine(testBackToOriginalPoint);
            // TaskRepeat(true);
        }

        private static void TaskRepeat(bool verbose, int n, bool isBinary = false, bool speedRun = true,
            int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            var counter = 0;
            for (var i = 0; i < n; i++)
            {
                var fitness = F7(isBinary, speedRun, dimensions, populationSize, iterations, mutationProbability,
                    precision, k, deviation);
                if (Math.Abs(fitness) < 10E-6)
                    counter++;
                if (verbose)
                    Console.WriteLine($"{fitness}");
            }

            Console.WriteLine($"Success percentage: {(counter / (double) n) * 100}%");
        }


        private static double Rosenbrock(bool isBinary = false, bool speedRun = true, int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            Console.WriteLine(
                $"{(isBinary ? "BINARY " : "DOUBLE ")}ROSENBROCK {dimensions} dimensions, {populationSize} individuals," +
                $" {iterations:N0} iterations, {mutationProbability * 100}% mutation probability, " +
                $"N({Mean}, {deviation}) Gaussian Normal Distribution, {k}-Tournament Selection, {precision} precision.");
            Console.WriteLine($"Speed running? {speedRun}");
            if (!isBinary)
            {
                var doubleGa = new DoubleGa(Functions.Rosenbrock, dimensions, populationSize, iterations,
                    LowerBound, UpperBound);
                var crossover = new BlxDoubleCrossover();
                var mutation = new GaussianDoubleMutation();
                var selection = new KTournamentSelection<double[]>(Functions.Rosenbrock, crossover, mutation,
                    LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = doubleGa.FindBestIndividual(speedRun, Functions.Rosenbrock, selection, crossover, mutation,
                    Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"Rosenbrock found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
            else
            {
                var binaryGa = new BinaryGa(Functions.Rosenbrock, dimensions, populationSize, iterations, LowerBound,
                    UpperBound, precision);
                var crossover = new UniformBinaryCrossover();
                var mutation = new SimpleBinaryMutation();
                var selection = new KTournamentSelection<uint[][]>(Functions.Rosenbrock, crossover, mutation,
                    LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = binaryGa.FindBestIndividual(speedRun, Functions.Rosenbrock, selection, crossover, mutation,
                    Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"Rosenbrock found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
        }

        private static double F3(bool isBinary = false, bool speedRun = true, int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            Console.WriteLine(
                $"{(isBinary ? "BINARY " : "DOUBLE ")}F3: {dimensions} dimensions, {populationSize} individuals," +
                $" {iterations:N0} iterations, {mutationProbability * 100}% mutation probability, " +
                $"N({Mean}, {deviation}) Gaussian Normal Distribution, {k}-Tournament Selection, {precision} precision.");
            Console.WriteLine($"Speed running? {speedRun}");
            if (!isBinary)
            {
                var doubleGa = new DoubleGa(Functions.F3, dimensions, populationSize, iterations, LowerBound,
                    UpperBound);
                var crossover = new BlxDoubleCrossover();
                var mutation = new GaussianDoubleMutation();
                var selection = new KTournamentSelection<double[]>(Functions.F3, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = doubleGa.FindBestIndividual(speedRun, Functions.F3, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F3 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
            else
            {
                var binaryGa = new BinaryGa(Functions.F3, dimensions, populationSize, iterations, LowerBound,
                    UpperBound, precision);
                var crossover = new UniformBinaryCrossover();
                var mutation = new SimpleBinaryMutation();
                var selection = new KTournamentSelection<uint[][]>(Functions.F3, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = binaryGa.FindBestIndividual(speedRun, Functions.F3, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F3 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
        }

        private static double F6(bool isBinary = false, bool speedRun = true, int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            Console.WriteLine(
                $"{(isBinary ? "BINARY " : "DOUBLE ")}F6: {dimensions} dimensions, {populationSize} individuals," +
                $" {iterations:N0} iterations, {mutationProbability * 100}% mutation probability, " +
                $"N({Mean}, {deviation}) Gaussian Normal Distribution, {k}-Tournament Selection, {precision} precision.");
            Console.WriteLine($"Speed running? {speedRun}");
            if (!isBinary)
            {
                var doubleGa = new DoubleGa(Functions.F6, dimensions, populationSize, iterations, LowerBound,
                    UpperBound);
                var crossover = new BlxDoubleCrossover();
                var mutation = new GaussianDoubleMutation();
                var selection = new KTournamentSelection<double[]>(Functions.F6, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = doubleGa.FindBestIndividual(speedRun, Functions.F6, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F6 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
            else
            {
                var binaryGa = new BinaryGa(Functions.F6, dimensions, populationSize, iterations, LowerBound,
                    UpperBound, precision);
                var crossover = new UniformBinaryCrossover();
                var mutation = new SimpleBinaryMutation();
                var selection = new KTournamentSelection<uint[][]>(Functions.F6, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = binaryGa.FindBestIndividual(speedRun, Functions.F6, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F6 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
        }

        private static double F7(bool isBinary = false, bool speedRun = true, int dimensions = 2,
            int populationSize = 150,
            int iterations = 1_000_000,
            double mutationProbability = 0.15, int precision = 6, int k = 3, double deviation = 0.01)
        {
            Console.WriteLine(
                $"{(isBinary ? "BINARY " : "DOUBLE ")}F7: {dimensions} dimensions, {populationSize} individuals," +
                $" {iterations:N0} iterations, {mutationProbability * 100}% mutation probability, " +
                $"N({Mean}, {deviation}) Gaussian Normal Distribution, {k}-Tournament Selection, {precision} precision.");
            Console.WriteLine($"Speed running? {speedRun}");
            if (!isBinary)
            {
                // Works well even with 80 dimensions with the commented scientific breakthrough.
                var doubleGa = new DoubleGa(Functions.F7, dimensions, populationSize, iterations, LowerBound,
                    UpperBound);
                var crossover = new BlxDoubleCrossover();
                var mutation = new GaussianDoubleMutation();
                var selection = new KTournamentSelection<double[]>(Functions.F7, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = doubleGa.FindBestIndividual(speedRun, Functions.F7, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F7 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
            else
            {
                var binaryGa = new BinaryGa(Functions.F7, dimensions, populationSize, iterations, LowerBound,
                    UpperBound, precision);
                var crossover = new UniformBinaryCrossover();
                var mutation = new SimpleBinaryMutation();
                var selection = new KTournamentSelection<uint[][]>(Functions.F7, crossover, mutation, LowerBound,
                    UpperBound, mutationProbability, k, precision, Mean, deviation);
                var best = binaryGa.FindBestIndividual(speedRun, Functions.F7, selection, crossover, mutation, Epsilon);
                var found = Math.Abs(best.Fitness) <= Epsilon;
                Console.WriteLine($"F7 found? {found}");
                Console.WriteLine(best);
                Console.WriteLine();
                return best.Fitness;
            }
        }
    }
}