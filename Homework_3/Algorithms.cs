using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Homework_3.OptimizationFunctions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3
{
    public static class Algorithms
    {
        private const double K = 0.61803398874989484820458683436;
        private const double EPSILON = 1E-6;
        private const double STEP = 1;
        private const double POINT = 1;
        private const int MAX_ITERATIONS = 5000;

        private static Interval UnimodalInterval(double h, double point, Func<double, double> function)
        {
            var l = point - h;
            var r = point + h;
            var m = point;
            uint step = 1;
            var fm = function(m);
            var fl = function(l);
            var fr = function(r);

            if (fm > fr)
                do
                {
                    l = m;
                    m = r;
                    fm = fr;
                    r = point + h * (step *= 2);
                    fr = function(r);
                } while (fm > fr);
            else
                do
                {
                    r = m;
                    m = l;
                    fm = fl;
                    l = point - h * (step *= 2);
                    fl = function(l);
                } while (fm > fl);

            var interval = new Interval {Left = l, Right = r};
            return interval;
        }

        private static Interval GoldenRatio(double point, double step, double e, Func<double, double> function)
            => GoldenRatio(UnimodalInterval(step, point, function), e, function);

        private static Interval GoldenRatio(Interval ab, double e, Func<double, double> function)
        {
            var a = ab!.Left;
            var b = ab!.Right;
            var c = b - K * (b - a);
            var d = a + K * (b - a);

            var fc = function(c);
            var fd = function(d);

            while ((b - a) > e)
            {
                if (fc < fd)
                {
                    b = d;
                    d = c;
                    c = b - K * (b - a);
                    fd = fc;
                    fc = function(c);
                }
                else
                {
                    a = c;
                    c = d;
                    d = a + K * (b - a);
                    fc = fd;
                    fd = function(d);
                }
            }

            var interval = new Interval {Left = a, Right = b};
            return interval;
        }

        public static Vector HookeJeeves(Vector x0, Func<Vector, double> function, double deltaX = 1,
            double e = EPSILON)
        {
            var xb = x0;
            var xp = x0;

            for (var i = 0; i < MAX_ITERATIONS; i++)
            {
                var xn = Seek(xp, deltaX, function);
                if (function(xn) < function(xb))
                {
                    xp = (Vector) (2 * xn - xb);
                    xb = xn;
                }
                else
                {
                    deltaX *= 0.5;
                    xp = xb;
                }

                if (deltaX <= e) break;
            }

            return xb;
        }

        private static Vector Seek(Vector xp, double deltaX, Func<Vector, double> function)
        {
            // value copy of xp, not a reference copy
            // var x = new Vector(xp.Point.Select(a => a).ToArray());
            var x = new DenseVector(xp.Count);
            xp.CopyTo(x);


            for (var i = 0; i < x.Count; i++)
            {
                var p = function(x);
                x[i] += deltaX;
                var n = function(x);

                // if N < P, skip iteration of for loop
                if (!(n > p)) continue;
                x[i] -= 2 * deltaX; // positive offset is not optimal, subtract by 2dx to counteract the +dx, so the net is -dx
                n = function(x);
                if (n > p)
                    x[i] += deltaX; // negative offset is not optimal, backtrack to the beginning
            }

            return x;
        }

        public static Vector GradientDescent(IOptimizationFunction function, Vector x0, bool useGoldenRatio = true,
            double epsilon = 10E-6)
        {
            var x = new DenseVector(x0.Count);
            x0.CopyTo(x);

            var iterations = 0;

            var minimalLambda = -1.0;
            for (var i = 0; i < MAX_ITERATIONS; i++)
            {
                double LambdaFunc(double lambda) =>
                    function.Function((Vector) (x + lambda * function.ComputeGradient(x)));

                if (useGoldenRatio)
                {
                    var golden = GoldenRatio(POINT, STEP, EPSILON, LambdaFunc);

                    minimalLambda = (golden.Left + golden.Right) / 2;
                }

                x = (DenseVector) (x + minimalLambda * function.ComputeGradient(x));

                iterations = i;
                if (function.ComputeGradient(x).L2Norm() < epsilon) break;
            }
            Console.WriteLine($"Number of iterations: {iterations}");
            return x;
        }

        public static Vector NewtonRaphson(IOptimizationFunction function, Vector x0, bool useGoldenRatio = true,
            double epsilon = 10E-6)
        {
            var x = new DenseVector(x0.Count);
            x0.CopyTo(x);

            var iterations = 0;

            var minimalLambda = -1.0;
            for (var i = 0; i < MAX_ITERATIONS; i++)
            {
                var delta = (Vector) (-function.ComputeHessian(x).Inverse() * function.ComputeGradient(x));

                double LambdaFunc(double lambda) =>
                    function.Function((Vector) (x + lambda * delta));

                if (useGoldenRatio)
                {
                    var golden = GoldenRatio(POINT, STEP, EPSILON, LambdaFunc);

                    minimalLambda = (golden.Left + golden.Right) / 2;
                }

                x = (DenseVector) (x + minimalLambda * delta);

                iterations = i;
                if (function.ComputeGradient(x).L2Norm() < epsilon) break;
            }

            Console.WriteLine($"Number of iterations: {iterations}");
            return x;
        }

        public static Vector Box(IOptimizationFunction function, Vector x0, List<Func<Vector, bool>> inequalities,
            Vector lowerBounds, Vector upperBounds, int alpha = 2,
            double epsilon = EPSILON)
        {
            var centroid = new DenseVector(x0.Count);
            x0.CopyTo(centroid);

            var iterations = 0;

            var simplex = GenerateSimplex(x0, x0, lowerBounds, upperBounds, inequalities);

            for (var j = 0; j < MAX_ITERATIONS; j++)
            {
                simplex = simplex.OrderByDescending(function.Function).ToList();
                centroid = (DenseVector) GenerateCentroid(simplex);

                var reflected = (Vector) ((1 + alpha) * centroid - alpha * simplex[0]);

                for (var i = 0; i < simplex.First().Count; i++)
                    reflected[i] = Math.Min(Math.Max(reflected[i], lowerBounds[i]), upperBounds[i]);

                while (!CheckInequalitiesVector(reflected, inequalities))
                    reflected = (Vector) (0.5 * (reflected + centroid));

                if (function.Function(reflected) > function.Function(simplex[1]))
                    reflected = (Vector) (0.5 * (reflected + centroid));

                simplex[0] = reflected;
                iterations = j;
                if (CalculateStopCondition(simplex, centroid, function.Function) < epsilon) break;
            }
            Console.WriteLine($"Number of iterations: {iterations}");
            return centroid;
        }

        public static Vector GenerateCentroid(IList<Vector> sortedSimplex)
        {
            var n = sortedSimplex.First().Count;
            var centroid = new DenseVector(n);

            // skip the first since the simplex is sorted
            for (var i = 1; i < sortedSimplex.Count; i++)
                centroid = (DenseVector) (centroid + sortedSimplex[i]);

            return centroid / (sortedSimplex.Count - 1);
        }

        public static double CalculateStopCondition(IList<Vector> simplex, Vector centroid,
            Func<Vector, double> function)
        {
            var n = simplex.Count;

            var quadraticSum = 0.0;

            for (var i = 0; i < n; i++)
                quadraticSum += Math.Pow(function(simplex[i]) - function(centroid), 2);

            var result = Math.Sqrt(quadraticSum / n);
            return result;
        }

        private static IList<Vector> GenerateSimplex(Vector centroid, Vector x1, Vector lowerBounds,
            Vector upperBounds,
            IReadOnlyCollection<Func<Vector, bool>> inequalities)
        {
            var n = centroid.Count;
            var random = new Random();
            var simplex = new List<Vector> {x1};

            for (var i = 1; i < 2 * n; i++)
                simplex.Add(new DenseVector(n));

            for (var j = 1; j < 2 * n; j++)
            {
                var r = random.NextDouble();
                simplex[j] = (Vector) (lowerBounds + r * (upperBounds - lowerBounds));

                while (!CheckInequalities(simplex, inequalities))
                    simplex[j] = (Vector) (0.5 * (simplex[j] + centroid));

                for (var i = 1; i < j; i++)
                    centroid = (Vector) (simplex[i] / j);
            }

            return simplex;
        }

        public static bool
            CheckInequalitiesVector(Vector vector, IEnumerable<Func<Vector, bool>> inequalities) =>
            inequalities.All(inequality => inequality(vector));

        public static bool CheckInequalities(IEnumerable<Vector> simplex, IEnumerable<Func<Vector, bool>> inequalities)
            => inequalities.All(simplex.All);

        public static Func<Vector, double> G(List<Func<Vector, double>> inequalities) =>
            x =>
            {
                var sum = 0.0;
                foreach (var inequality in inequalities)
                    if (inequality(x) < 0)
                        sum -= inequality(x);

                return sum;
            };

        public static Vector InnerPoint(List<Func<Vector, double>> inequalities, Vector x0, int t = 1)
        {
            var x = new DenseVector(x0.Count);
            x0.CopyTo(x);
            var xs = new DenseVector(x0.Count);
            x0.CopyTo(xs);

            for (var i = 0; i < MAX_ITERATIONS; i++)
            {
                xs = x;
                x = (DenseVector) HookeJeeves(x, G(inequalities));
                if ((xs - x).L2Norm() < EPSILON) break;
            }

            return x;
        }

        public static Func<Vector, double> F(Func<Vector, double> f,
            IEnumerable<Func<Vector, double>> inequalities,
            IEnumerable<Func<Vector, double>> equalities, int t)
            => x =>
            {
                var fx = f(x);
                foreach (var inequality in inequalities)
                {
                    if (inequality(x) < 0)
                        return double.PositiveInfinity;
                    fx -= (1.0 / t) * Math.Log(inequality(x));
                }

                foreach (var equality in equalities)
                    fx += t * Math.Pow(equality(x), 2);

                return fx;
            };

        private static bool CheckIneqVector(Vector x, List<Func<Vector, double>> ineqs) =>
            ineqs.All(inequality => inequality(x) > 0);

        public static Vector Barrier(Vector x0, Func<Vector, double> function, List<Func<Vector, double>> inequalities,
            List<Func<Vector, double>> equalities, int t0 = 1)
        {
            var x = (Vector) x0.Clone();

            if (!CheckIneqVector(x, inequalities))
                x = InnerPoint(inequalities, x);

            var t = t0;
            var xs = (Vector) x0.Clone();
            var iteration = 0;
            for (var i = 0; i < MAX_ITERATIONS; i++)
            {
                xs = x;
                x = HookeJeeves(x, F(function, inequalities, equalities, t));
                t = 10 * t;
                iteration = i;
                if ((xs - x).L2Norm() < EPSILON) break;
            }

            Console.WriteLine($"Number of iterations: {iteration}");
            return x;
        }
    }
}