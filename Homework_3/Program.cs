using System;
using System.Collections.Generic;
using System.Linq;
using Homework_3.OptimizationFunctions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Assignment1();
            Assignment2();
            Assignment3();
            Assignment4();
            Assignment5();
        }

        private static void Assignment1()
        {
            Console.WriteLine("--------------ASSIGNMENT [1]--------------------");
            Console.WriteLine("Starting vector:");
            var x = new DenseVector(new[] { 0.0, 0.0 });
            Console.WriteLine(x);
            var function = new Function3();
            Console.WriteLine(function);

            Console.WriteLine("OPTIMAL DESCENT");
            var optimalDescent = Algorithms.GradientDescent(function, x, useGoldenRatio: true, epsilon: 10E-6);
            Console.WriteLine(optimalDescent);

            Console.WriteLine();
            Console.WriteLine("NON-OPTIMAL DESCENT");
            var nonOptimalDescent = Algorithms.GradientDescent(function, x, useGoldenRatio: false, epsilon: 10E-6);
            Console.WriteLine(nonOptimalDescent);

            Console.WriteLine("--------------------END----------------------");
        }

        private static void Assignment2()
        {
            Console.WriteLine("--------------ASSIGNMENT [2]--------------------");
            Console.WriteLine("Starting vector 1:");
            var x1 = new DenseVector(new[] { -1.9, 2.0 });
            Console.WriteLine(x1);
            Console.WriteLine("Starting vector 2:");
            Console.WriteLine();
            var x2 = new DenseVector(new[] { 0.1, 0.3 });
            Console.WriteLine(x2);
            
            Console.WriteLine();
            var function1 = new Rosenbrock();
            Console.WriteLine(function1);
            Console.WriteLine();
            var function2 = new Function2();
            Console.WriteLine(function2);

            Console.WriteLine("ROSENBROCK ALGORITHMS: optimal gradient descent & Newton Raphson");
            var optimalDescentRosenbrock = Algorithms.GradientDescent(function1, x1);
            var newtonRaphsonRosenbrock = Algorithms.NewtonRaphson(function1, x1);
            Console.WriteLine(optimalDescentRosenbrock);
            Console.WriteLine(newtonRaphsonRosenbrock);
            Console.WriteLine();
            
            Console.WriteLine("F2 ALGORITHMS: optimal gradient descent & Newton Raphson");
            var optimalDescentF2 = Algorithms.GradientDescent(function2, x2);
            var newtonRaphsonF2 = Algorithms.NewtonRaphson(function2, x2);
            Console.WriteLine(optimalDescentF2);
            Console.WriteLine(newtonRaphsonF2);
            Console.WriteLine("--------------------END----------------------");
        }

        private static void Assignment3()
        {
            Console.WriteLine("--------------ASSIGNMENT [3]--------------------");
            Console.WriteLine("Starting vector 1:");
            var x1 = new DenseVector(new[] { -1.9, 2.0 });
            Console.WriteLine(x1);
            Console.WriteLine("Starting vector 2:");
            Console.WriteLine();
            var x2 = new DenseVector(new[] { 0.1, 0.3 });
            Console.WriteLine(x2);
            
            Console.WriteLine();
            var function1 = new Rosenbrock();
            Console.WriteLine(function1);
            
            Console.WriteLine();
            
            var function2 = new Function2();
            Console.WriteLine(function2);
            
            static bool InequalityOne(Vector x) => x[1] - x[0] >= 0;
            static bool InequalityTwo(Vector x) => 2 - x[0] >= 0;
            var inequalities = new List<Func<Vector, bool>>
            {
                InequalityOne,
                InequalityTwo
            };
            
            const double lBound = -100.0;
            const double uBound = 100.0;
            var lBounds = new List<double>();
            var uBounds = new List<double>();
            for (var i = 0; i < x1.Count; i++)
            {
                lBounds.Add(lBound);
                uBounds.Add(uBound);
            }
            var lowerBounds = new DenseVector(lBounds.ToArray());
            var upperBounds = new DenseVector(uBounds.ToArray());

            Console.WriteLine("ROSENBROCK BOX:");
            var boxRosenbrock = Algorithms.Box(function1, x1, inequalities, lowerBounds, upperBounds);
            Console.WriteLine(boxRosenbrock);
            Console.WriteLine();
            
            Console.WriteLine("F2 BOX:");
            var boxF2 = Algorithms.Box(function2, x2, inequalities, lowerBounds, upperBounds);
            Console.WriteLine(boxF2);
            Console.WriteLine("--------------------END----------------------");
        }

        private static void Assignment4()
        {
            Console.WriteLine("--------------ASSIGNMENT [4]--------------------");
            Console.WriteLine("Starting vector 1:");
            var x1 = new DenseVector(new[] { -1.9, 2.0 });
            Console.WriteLine(x1);
            Console.WriteLine("Starting vector 2:");
            Console.WriteLine();
            var x2 = new DenseVector(new[] { 0.1, 0.3 });
            Console.WriteLine(x2);
            
            Console.WriteLine();
            var function1 = new Rosenbrock();
            Console.WriteLine(function1);
            
            var function2 = new Function2();
            Console.WriteLine(function2);
            
            static double InequalityOne(Vector x) => x[1] - x[0];
            static double InequalityTwo(Vector x) => 2 - x[0];
            var inequalities = new List<Func<Vector, double>>
            {
                InequalityOne,
                InequalityTwo
            };

            Console.WriteLine("ROSENBROCK BARRIER:");
            var barrierRosenbrock =
                Algorithms.Barrier(x1, function1.Function, inequalities, new List<Func<Vector, double>>());
            Console.WriteLine(barrierRosenbrock);
            Console.WriteLine();
            Console.WriteLine("F2 BARRIER:");
            var barrierF2 = Algorithms.Barrier(x2, function2.Function, inequalities, new List<Func<Vector, double>>());
            Console.WriteLine(barrierF2);
            Console.WriteLine("--------------------END----------------------");
        }

        public static void Assignment5()
        {
            Console.WriteLine("--------------ASSIGNMENT [5]--------------------");
            Console.WriteLine("Starting vector 1:");
            var x1 = new DenseVector(new[] { 0.0, 0.0 });
            Console.WriteLine(x1);
            Console.WriteLine();
            
            Console.WriteLine("Starting vector 2:");
            var x2 = new DenseVector(new[] { 5.0, 5.0 });
            Console.WriteLine(x2);
            var function = new Function4();
            Console.WriteLine(function);
            Console.WriteLine();
            Func<Vector, double> InequalityOne = x => 3 - x[0] - x[1];
            Func<Vector, double> InequalityTwo = x => 3 + 1.5 * x[0] - x[1];

            var inequalities = new List<Func<Vector, double>>
            {
                InequalityOne,
                InequalityTwo,
            };
            
            Func<Vector, double> Equality = x => x[1] - 1;

            var equalities = new List<Func<Vector, double>>
            {
                Equality,
            };
            
            Console.WriteLine("F4 BARRIER: X1 & X2");
            var barrier1 = Algorithms.Barrier(x1, function.Function, inequalities, equalities);
            Console.WriteLine(barrier1);

            var barrier2 = Algorithms.Barrier(x2, function.Function, inequalities, equalities);
            Console.WriteLine(barrier2);
            Console.WriteLine("--------------------END----------------------");
        }
    }
}