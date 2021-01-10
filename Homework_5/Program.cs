using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_5
{
    class Program
    {
        private static readonly VectorBuilder<double> VectorBuilder = Vector<double>.Build;
        private static readonly MatrixBuilder<double> MatrixBuilder = Matrix<double>.Build;

        static void Main(string[] args)
        {
            // Assignment1(verbose: false);
            // Assignment2(verbose: false);
            // Assignment3(verbose: false);
            Assignment4(verbose: false);
        }

        private static void Assignment1(double T = 0.01, int tmax = 10, bool verbose = true)
        {
            Console.WriteLine("ASSIGNEMENT 1");
            var start = VectorBuilder.Dense(new[] {1.0, 1.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, -1.0, 1.0, 0.0});
            var B = MatrixBuilder.Dense(2, 2);
            Func<double, double> rt = _ => 0.0;

            var euler = Euler(tmax, T, start, A, B, rt, verbose);
            var reverseEuler = ReverseEuler(tmax, T, start, A, B, rt, verbose);
            var trapezoid = Trapezoid(tmax, T, start, A, B, rt, verbose);
            var rungeKutta = RungeKutta(tmax, T, start, A, B, rt, verbose);
        }

        private static void Assignment2(double T = 0.1, int tmax = 1, bool verbose = true)
        {
            Console.WriteLine("ASSIGNMENT 2");
            var start = VectorBuilder.Dense(new[] {1.0, -2.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, -200.0, 1.0, -102.0});
            var B = MatrixBuilder.Dense(2, 2);
            Func<double, double> rt = _ => 0.0;

            var euler = Euler(tmax, T, start, A, B, rt, verbose);
            var reverseEuler = ReverseEuler(tmax, T, start, A, B, rt, verbose);
            var trapezoid = Trapezoid(tmax, T, start, A, B, rt, verbose);
            var rungeKutta = RungeKutta(tmax, T, start, A, B, rt, verbose);
        }

        private static void Assignment3(double T = 0.01, int tmax = 10, bool verbose = true)
        {
            Console.WriteLine("ASSIGNMENT 3");
            var start = VectorBuilder.Dense(new[] {1.0, 3.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, 1.0, -2.0, -3.0});
            var B = MatrixBuilder.Dense(2, 2, new[] {2.0, 0.0, 0.0, 3.0});
            Func<double, double> rt = _ => 1.0;

            var euler = Euler(tmax, T, start, A, B, rt, verbose);
            var reverseEuler = ReverseEuler(tmax, T, start, A, B, rt, verbose);
            var trapezoid = Trapezoid(tmax, T, start, A, B, rt, verbose);
            var rungeKutta = RungeKutta(tmax, T, start, A, B, rt, verbose);
        }

        private static void Assignment4(double T = 0.01, int tmax = 1, bool verbose = true)
        {
            Console.WriteLine("ASSIGNMENT 4");
            var start = VectorBuilder.Dense(new[] {-1.0, 3.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {1.0, 1.0, -5.0, -7.0});
            var B = MatrixBuilder.Dense(2, 2, new[] {5.0, 0.0, 0.0, 3.0});
            Func<double, double> rt = t => t;

            var euler = Euler(tmax, T, start, A, B, rt, verbose);
            var reverseEuler = ReverseEuler(tmax, T, start, A, B, rt, verbose);
            var trapezoid = Trapezoid(tmax, T, start, A, B, rt, verbose);
            var rungeKutta = RungeKutta(tmax, T, start, A, B, rt, verbose);
        }

        private static Vector<double> Euler(double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine($"\nEULER(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += T * A * x;

                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");

            return x;
        }

        private static Vector<double> ReverseEuler(double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine($"\nREVERSE EULER(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var P = (U - A * T).Inverse();
            var Q = P * T * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = P * x + Q * VectorBuilder.Dense(new[] {r(t), r(t)});
                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");

            return x;
        }

        private static Vector<double> Trapezoid(double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine($"\nTRAPEZOID(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var R = (U - A * 0.5 * T).Inverse() * (U + A * 0.5 * T);

            var S = (U - A * 0.5 * T).Inverse() * (0.5 * T) * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = R * x + S * VectorBuilder.Dense(new[] {r(t - T) + r(t), r(t - T) + r(t)});

                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");

            return x;
        }

        private static Vector<double> RungeKutta(double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine($"\nRUNGE KUTTA(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                var tk = t - T;
                var m1 = A * x + B * VectorBuilder.Dense(new[] {r(tk), r(tk)});
                var m2 = A * (x + (T / 2.0) * m1) +
                         B * VectorBuilder.Dense(new[] {r(tk + (T / 2.0)), r(tk + (T / 2.0))});
                var m3 = A * (x + (T / 2.0) * m2) +
                         B * VectorBuilder.Dense(new[] {r(tk + (T / 2.0)), r(tk + (T / 2.0))});
                var m4 = A * (x + T * m3) + B * VectorBuilder.Dense(new[] {r(tk + T), r(tk + T)});

                x += (T / 6.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                cumulativeError += Vector<double>.Abs(x - pendulum);
                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            return x;
        }
    }
}